\# Key Event Logger – C# Windows Forms



YMH 219 Nesne Tabanlı Programlama dersi ek uygulama ödevi kapsamında geliştirilmiş eğitsel bir klavye olayı kaydedicisidir. Uygulama, \*\*yalnızca kendi penceresi aktifken\*\* basılan tuşları tarih-saat bilgisiyle listeler, istatistik çıkarır ve kullanıcı isterse kayıtları `.txt` dosyasına kaydeder.



\*\*Öğrenci:\*\* Hasan Kara – 250541015



\## Ekran Görüntüleri



| Kayıt Ekranı | Kaydedilen Dosya |

|---|---|

| !\[Kayıt ekranı](screenshots/kayit.png) | !\[TXT dosyası](screenshots/txt\_dosyasi.png) |



\## Kullanılan Teknolojiler



\- C#

\- .NET 8

\- Windows Forms

\- `KeyDown` olayı ve `KeyPreview` özelliği

\- `StreamWriter` ile dosya işlemleri

\- LINQ (`GroupBy`, `OrderByDescending`) ile istatistik hesaplama



\## Kurulum ve Çalıştırma



1\. Depoyu klonlayın:git clone https://github.com/Hasankr-27/YMH219-KeyEventLogger.git

2. `YMH219-KeyEventLogger.sln` dosyasını Visual Studio 2022 veya üzeriyle açın.
3. .NET 8 SDK yüklü değilse Visual Studio'nun önerdiği şekilde yükleyin.
4. `F5` ile çalıştırın.

## Kullanım

1. **Başlat** butonuyla kaydı açın (gösterge yeşile döner).
2. Yazı alanına yazın; her tuş saatiyle birlikte listeye eklenir.
3. **Durdur** ile kaydı durdurun, **Temizle** ile listeyi silin.
4. **Kaydet** ile kayıtları istediğiniz konuma `.txt` olarak kaydedin.

## Özellikler

- Tuş adı ve milisaniye hassasiyetinde zaman bilgisiyle listeleme
- `Ctrl`, `Alt`, `Shift` kombinasyonlarının okunabilir gösterimi (ör. `Ctrl + C`)
- Başlat / Durdur / Temizle kontrolleri
- Renkli kayıt durumu göstergesi (**KAYIT AÇIK** / **KAYIT KAPALI**)
- Kayıtları `.txt` dosyasına kaydetme (başlık, tarih, toplam tuş ve istatistik ile)
- Hata yönetimi: boş liste uyarısı, yazma izni olmayan klasör ve dosya hatalarında program çökmeden mesaj gösterilir
- Toplam tuş sayısı ve en çok basılan 5 tuşun anlık istatistiği

## Proje Yapısı

| Sınıf | Görevi |
|---|---|
| `KeyLogEntry` | Tek bir kayıt: tuş adı ve zaman bilgisi |
| `KeyLogger` | Kayıt listesi, kayıt durumu, istatistik hesaplama ve dosyaya kaydetme |
| `Form1` | Arayüz, klavye olaylarının yakalanması ve ekranın güncellenmesi |

## Kapsam ve Güvenlik Sınırı

Bu uygulama gerçek bir sistem keylogger'ı **değildir**:

- Tuşlar yalnızca uygulamanın kendi penceresi odaktayken yakalanır (global keyboard hook kullanılmaz).
- Arka planda veya gizli çalışmaz, bilgisayar açılışında otomatik başlamaz.
- Başka uygulamalardan veri toplamaz, kayıtları ağ üzerinden göndermez.
- Kayıtlar yalnızca kullanıcı **Kaydet** butonuna bastığında, kullanıcının seçtiği konuma yazılır.

## Bilinen Sınırlamalar

- Kayıtlar yalnızca uygulama açıkken bellekte tutulur; kaydedilmeden kapatılırsa silinir.
- Tuş adları `Keys` enum isimleriyle gösterilir (ör. `OemPeriod`, `D1`), Türkçe karakterler ayrıca gösterilmez.
- Kayıtları tarih/saat aralığına göre filtreleme özelliği yoktur.

