#include <stdio.h>
#include <stdlib.h>
#include <string.h>

struct Person
{
	char Name[50];
	char Surname[50];
	char Identity[12];
	char Phone[13];
	int Age;
	int Id;
};


int GetAge(struct Person* p) {
	return p->Age;
}

const char* GetIdentity(struct Person* p) {
	return p->Identity;
}

const char* GetPhone(struct Person* p) {
	return p->Phone;
}

const char* GetName(struct Person* p) {
	return p->Name;
}

const char* GetSurname(struct Person* p) {
	return p->Surname;
}

int SetAge(struct Person* p, int age) {
	p->Age = age;
	return 1;
}

int SetId(struct Person* p, int id) {
	p->Id = id;
	return 1;
}

int SetName(struct Person* p, const char* newName) {
	strncpy(p->Name, newName, sizeof(p->Name) - 1);
	p->Name[sizeof(p->Name) - 1] = '\0';
	return 1;
}

int SetIdentity(struct Person* p, const char* newIdentity) {
	strncpy(p->Identity, newIdentity, sizeof(p->Identity) - 1);
	p->Identity[sizeof(p->Identity) - 1] = '\0';
	return 1;
}

int SetPhone(struct Person* p, const char* newPhone) {
	strncpy(p->Phone, newPhone, sizeof(p->Phone) - 1);
	p->Phone[sizeof(p->Phone) - 1] = '\0';
	return 1;
}

int SetSurName(struct Person* p, const char* newSurname) {
	strncpy(p->Surname, newSurname, sizeof(p->Surname) - 1);
	p->Surname[sizeof(p->Surname) - 1] = '\0';
	return 1;
}

int AddNewPerson(struct Person* p) {
	char na[50], sna[50],ide[11],pho[11];
	int ag;

	printf("Lutfen musteri adi giriniz : ");
	scanf("%49s", &na);

	printf("Lutfen musteri Soyadini giriniz : ");
	scanf("%49s", &sna);

	printf("Lutfen musteri Yasini giriniz : ");
	scanf("%d", &ag);

	printf("Lutfen musteri Tc Nosunu giriniz : ");
	scanf("%11s", &ide);

	printf("Lutfen musteri Telefon Numarasýný giriniz : ");
	scanf("%11s", &pho);

	SetName(p, na);
	SetSurName(p, sna);
	SetAge(p, ag);
}

int main() {
	struct Person p[50];
	memset(p, 0, sizeof(p));
	char Comm[10];

	while (1) {

		printf("Yeni Kayit Icin 'New'\n");
		printf("Kayitlari Getirmek Icin 'List'\n");
		printf("Cikmak icin 'Exit'\n");


		scanf("%9s", Comm);
		if (strcmp(Comm, "New") == 0)
		{
			system("cls");
			for (int i = 0; i < 50; i++)
			{
				if (p[i].Age == 0)
				{
					AddNewPerson(&p[i]);
					SetId(&p[i], i + 1);
					break;
				}
			}

		}

		else if (strcmp(Comm, "Exit") == 0)
			break;


		else if (strcmp(Comm, "List") == 0) {
			system("cls");
			for (int i = 0; i < 50; i++)
			{
				if (p[i].Age != 0) {
					printf("%s\n", GetSurname(&p[i]));
					printf("%s\n", GetName(&p[i]));
					printf("%d\n", GetAge(&p[i]));
					printf("%d\n", GetPhone(&p[i]));
					printf("%d\n", GetIdentity(&p[i]));
				}
				else
					continue;
			}
		}
		
	}



	return 0;
}



int MallOcIleBellekTahsisi()
{

	float* ptrs = (float*)malloc(100 * sizeof(float));

	if (ptrs == NULL) {
		printf("Bellek tahsis edilemedi !\n");
		return 1;
	}

	printf("Bellek tahsis edildi o_0\n");
	for (int i = 0; i < 400; i++)
	{
		printf("%d 'inci : %p\n", i, &ptrs[i]);
	}

	printf("Bellek serbest birakildi !");
	free(ptrs);
	return 1;
}


int Adresleme() {
	int* pc, c;
	c = 5;
	printf("Deðer = %d\n", c);

	pc = &c;
	printf("Pointer = %d\n", *pc);

	c = 11;
	printf("Deðer = %d\n", c);

	*pc = 2;

	printf("Pointer = %d\n", *pc);
	printf("Deðer = %d\n", c);
	return 0;
}