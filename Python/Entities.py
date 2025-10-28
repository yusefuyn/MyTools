class Kedi():
    def __init__(self,isim):
        self.__isim = isim

    def Miyavla(self):
        print(f"{self.__isim} : Miyav")

    def Mirla(self):
        print(f"{self.__isim} : Mır")

    def AdiniDegistir(self,isim):
        self.__isim = isim
        
class Oyuncu():
    def __init__(self,isim,level):
        self.__level = level
        self.__isim = isim

    def LevelAtlat(self):
        self.__level += 1
        print(f"{self.__isim} isimli oyuncu {self.__level} oldu")

    def LevelDusur(self):
        self.__level -= 1
        print(f"{self.__isim} isimli oyuncu {self.__level}'e düstü.")

    def Level(self):
        return self.__level

class SuperOyuncu(Oyuncu):
    def __init__(self,isim,level):
        self.__level = level
        self.__isim = isim

    
    def LevelAtlat(self):
        self.__level += 2
        print(f"{self.__isim} isimli oyuncu {self.__level} oldu")

    def LevelDusur(self):
        self.__level -= 2
        print(f"{self.__isim} isimli oyuncu {self.__level}'e düstü.")

    def Level(self):
        return self.__level