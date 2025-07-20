// CMakeProject1.cpp: Uygulamanın giriş noktasını tanımlar.
//

#include "CMakeProject1.h"
#include <iostream>
#include <string>
#include <cstring>

#ifdef _WIN32
#include <winsock2.h>
#include <ws2tcpip.h>
#pragma comment(lib, "ws2_32.lib")
#else
#include <sys/socket.h>
#include <netinet/in.h>
#include <unistd.h>
#include <arpa/inet.h>
#endif



std::string readClientRequest(int clientSocket) {
	char buffer[4096];
	std::string request;

#ifdef _WIN32
	int bytesReceived = recv(clientSocket, buffer, sizeof(buffer), 0);
#else
	int bytesReceived = read(clientSocket, buffer, sizeof(buffer));
#endif

	if (bytesReceived > 0) {
		request.assign(buffer, bytesReceived);
	}
	return request;
}

std::string extractPath(const std::string& request) {
	// Örnek istek: "GET /sayfa/bilmemne HTTP/1.1\r\nHost: ..."
	size_t start = request.find(' ');
	if (start == std::string::npos) return "/";

	size_t end = request.find(' ', start + 1);
	if (end == std::string::npos) return "/";

	return request.substr(start + 1, end - start - 1);
}

void handleClient(int clientSocket) {
	std::string request = readClientRequest(clientSocket);
	std::string path = extractPath(request);

	std::cout << "İstenen path: " << path << std::endl;

	std::string response;
	if (path == "/") {
		response =
			"HTTP/1.1 200 OK\r\n"
			"Content-Type: text/html\r\n"
			"\r\n"
			"<h1>Su anda c++ ile ayağa kaldırılmış direk tcp ile canlı soket bağlantısındasınız.</h1>";
	}
	else if (path == "/sayfa/bilmemne") {
		response =
			"HTTP/1.1 200 OK\r\n"
			"Content-Type: text/html\r\n"
			"\r\n"
			"<h1>Bilmemne Sayfası</h1>"
			"<p>Özel içerik burada</p>";
	}
	else {
		response =
			"HTTP/1.1 404 Not Found\r\n"
			"Content-Type: text/html\r\n"
			"\r\n"
			"<h1>404 Sayfa Bulunamadı</h1>"
			"<p>" + path + " mevcut değil</p>";
	}

#ifdef _WIN32
	send(clientSocket, response.c_str(), response.length(), 0);
#else
	write(clientSocket, response.c_str(), response.length());
#endif
}

int main() {
#ifdef _WIN32
	// Windows için Winsock başlatma
	WSADATA wsaData;
	if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) {
		std::cerr << "WSAStartup basarisiz.\n";
		return 1;
	}
#endif

	// Socket oluştur
#ifdef _WIN32
	SOCKET serverSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (serverSocket == INVALID_SOCKET) {
		std::cerr << "Socket olusturulamadi.\n";
#ifdef _WIN32
		WSACleanup();
#endif
		return 1;
	}
#else
	int serverSocket = socket(AF_INET, SOCK_STREAM, 0);
	if (serverSocket < 0) {
		std::cerr << "Socket olusturulamadi.\n";
		return 1;
	}
#endif

	// Socket ayarları
	sockaddr_in serverAddress;
	memset(&serverAddress, 0, sizeof(serverAddress));
	serverAddress.sin_family = AF_INET;
	serverAddress.sin_addr.s_addr = INADDR_ANY;
	serverAddress.sin_port = htons(8080);

	// Socket'i bağla
	if (bind(serverSocket, (struct sockaddr*)&serverAddress, sizeof(serverAddress)) ){
		std::cerr << "Bind hatasi.\n";
#ifdef _WIN32
		closesocket(serverSocket);
		WSACleanup();
#else
		close(serverSocket);
#endif
		return 1;
	}

	// Dinlemeye başla
	if (listen(serverSocket, 5) < 0) {
		std::cerr << "Listen hatasi.\n";
#ifdef _WIN32
		closesocket(serverSocket);
		WSACleanup();
#else
		close(serverSocket);
#endif
		return 1;
	}

	std::cout << "HTTP sunucusu 8080 portunda dinleniyor...\n";
	std::cout << "Tarayıcıdan http://localhost:8080 adresini ziyaret edebilirsiniz.\n";


	while (true) {
		// İstemci bağlantısını kabul et
		sockaddr_in clientAddress;
#ifdef _WIN32
		int clientAddrLen = sizeof(clientAddress);
		SOCKET clientSocket = accept(serverSocket, (SOCKADDR*)&clientAddress, &clientAddrLen);
		if (clientSocket == INVALID_SOCKET) {
#else
		socklen_t clientAddrLen = sizeof(clientAddress);
		int clientSocket = accept(serverSocket, (struct sockaddr*)&clientAddress, &clientAddrLen);
		if (clientSocket < 0) {
#endif
			std::cerr << "Accept hatasi.\n";
			continue;
		}

		// İstemci bilgilerini yazdır
		char clientIP[INET_ADDRSTRLEN];
		inet_ntop(AF_INET, &(clientAddress.sin_addr), clientIP, INET_ADDRSTRLEN);
		std::cout << "Yeni baglanti: " << clientIP << ":" << ntohs(clientAddress.sin_port) << "\n";
		// HTTP yanıtı gönder
		handleClient(clientSocket);

#ifdef _WIN32
		closesocket(clientSocket);
#else
		close(clientSocket);
#endif
		}

	// Sunucuyu kapat (pratikte buraya ulaşılmaz)
#ifdef _WIN32
	closesocket(serverSocket);
	WSACleanup();
#else
	close(serverSocket);
#endif

	return 0;
}
