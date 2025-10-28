import random
import Entities

def Selamla(isim):
    return "Merhaba "+isim


def oncekiKodlar():
    isim = input("Adınızı giriniz :")
    print(Selamla(isim))
    print("Merhaba " + isim)
    yas = float(input("yaşınızı giriniz :"))
    if (yas < 18):
        print("çık buradan")
    else: 
        print("tamam")
    randomSayi = random.randint(1,9)
    print(randomSayi)

def oncekindensonrakikodlar():
    tekir = Entities.Kedi()
    tekir.Miyavla()
    tekir.Mirla()


oyuncu1 = Entities.Oyuncu("Test",5)
oyuncu1.LevelAtlat()
oyuncu1.LevelDusur()
oyuncu1.LevelDusur()
print(oyuncu1.Level())


oyuncu2 = Entities.SuperOyuncu("test s",5)
oyuncu2.LevelAtlat()
oyuncu2.LevelAtlat()
oyuncu2.LevelDusur()
print(oyuncu2.Level())
