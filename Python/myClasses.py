from sklearn.model_selection import train_test_split
from sklearn.linear_model import LinearRegression
from sklearn.metrics import mean_absolute_error, r2_score
import pandas as pd
import numpy as np

# Rastgele veri oluştur (örnek olarak)
np.random.seed(42)
df = pd.DataFrame({
    "oda": np.random.randint(1, 6, 100),
    "salon": np.random.randint(1, 3, 100),
    "tuvalet": np.random.randint(1, 3, 100),
    "kat": np.random.randint(1, 10, 100),
    "metrekare":np.random.randint(1, 130, 100),
    "lokasyon": np.random.randint(1, 101, 100),
})

# Fiyatı simüle edelim (örnek formül)
df["fiyat"] = (df["oda"] * 100 +
               df["salon"] * 50 +
               df["tuvalet"] * 30 +
               df["kat"] * 75 +
               df["metrekare"] * 5 +
               df["lokasyon"] * 50 +
               np.random.randint(-20000, 20000, 100))

df.head()

# Bağımsız ve bağımlı değişkenleri ayır
X = df[["oda", "salon", "tuvalet", "kat", "metrekare", "lokasyon"]]
y = df["fiyat"]

# Eğitim ve test ayırımı
X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)

# Model oluştur ve eğit
model = LinearRegression()
model.fit(X_train, y_train)

# Test tahmini
y_pred = model.predict(X_test)

# Performans ölç
mae = mean_absolute_error(y_test, y_pred)
r2 = r2_score(y_test, y_pred)

print("Ortalama Hata (MAE):", mae)
print("R^2 Skoru (Başarı):", r2)

# Oda, salon, tuvalet, kat, lokasyon
yeni_ev = [[3, 1, 2, 5, 80, 80]]
tahmin = model.predict(yeni_ev)
print("Tahmini fiyat:", round(tahmin[0], 2), "TL")