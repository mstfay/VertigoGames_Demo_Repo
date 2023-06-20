# Demo Wheel Game

Oyun, Critical Strike oyununda bulunan kart oyununun bir benzeri olarak çark çevirme oyunu demosudur.

### Projeden Ne Anladım?

Bu oyunda her bölgede, içeriği farklı olacak şekilde çarklar bulunuyor ve bu çarkları çeviren oyuncu çeşitli ödüller kazanabiliyor. Ancak bazı kurallar mevcuttu. Bunlar:

- Çark içerikleri her bölgede farklı olması gerekiyor.
- Oyunda, her 30. bölge, bomba içermeyen bir süper bölge. Her 5. bölge, bomba içermeyen güvenli bölge ve diğer bölgeler ise bomba içeren standart bölgeler.
- Oyuncu süper bölgedeyken gold bir çark, güvenli bölgede iken silver bir çark ve standart bir bölgede iken bronze bir çark çeviriyor.
- Oyuncu, kendisi istemediği veya bomba ile karşılaşmadığı sürece çarktan çıkan ödülleri biriktiriyor. Eğer bomba ile karşılaşırsa biriktirdiği tüm ödüller kayboluyor (bonus durum hariç) yada biriktirdiği ödülleri, alıp çark çevirmeyi sonlandırıyor.
- Oyuncu çarkın dönmemesi koşulu ile herhangi bir bölgede ödülleri alarak ayrılabiliyor.
#
### Ekranda Oyuncuyu Neler Karşılıyor?

- Ekranın orta kısmında oyuncunun kaderini belirleyen ve ödülleri gösteren bir çark duruyor. Spin butonuna basarak bu çarkı döndürmeye başlıyoruz ve indicatorün gösterdiği ödülü kaydediyoruz.
- Ekranın sol kısmında biriktirdiğimiz ödüllerin bilgilerini bize gösteren bir panel mevcut. Bu panel bir scroll içeriyor. Böylede toplanan ödül fazla ise hepsini görebiliyoruz.
- Ekranın üst kısmında hangi bölgede olduğumuzu gösteren bir panel bulunuyor. Ayrıca renkli olmaları sayesinde bölgenin türünü bilebiliyoruz.
- Ekranın sağ kısmında gelecek olan süper bölge ve güvenli bölgelerin numaralarını içeren bir panel bulunuyor.
- Ayrıca oyundan ayrılabilmek için sol panelde bir buton bulunuyor. Bu buton ile biriktirdiğimiz ödülleri toplayıp oyundan ayrılmamızı sağlayan bir panel açılıyor.(Ödül toplama işlemi yapılmamıştır. Butona basarsak yalnızca log atıyor.)
#
### Neler Yaptım? Nasıl Bir Yol İzledim?

- Öncelikle izlenmesi ve öğrenilmesi gereken listedeki videoları izleyerek ve linklerdeki bilgileri okuyarak başladım. Daha sonra projeye başladım.
- Farklı spinnerlar için ScriptableObjectler oluşturdum. Çarklardaki bölmeler 8 adet olduğu için buralarda bronze' dan gold' a doğru ödül miktarı ve çeşidini arttırdım ve bronze çark için 1 bomba öğesini koydum. Burada her bir ödül için görsel, tür ve sayısını tutan değerler atadım.
- Daha sonra SpinnerSettings isimli bir ScriptableObject oluşturdum. Bunun sebebi çarkların bölgeleri tipleri gibi verileri depolamaktı ve yönetim kolaylığı sağlamaktı.
- Ekranda bulunan panelleri oluşturdum ve bulunduğumuz bölgeye istinaden çarkın ve içeriğinin güncellendiği bir script yazdım.
- Her bir bölge için ayrı bir çark oluşturmamın istenmediğini düşündüğüm için, mevcut ScriptableObject içerisindeki ödül listesini random bir listeye döktüm ve çarkın dilimlerine yerleştirdim.
- Her bölge için ayrı bir çark oluşturmak istenebileceğini düşünerek SpinnerSettings' e "isRandomOrderActive" isimli bir bool koydum. Ancak vakitten dolayı bu kısmı tamamlayamadım.
- Her bir öğenin çıkma olasılığı gibi bir şart konulmadığı için spinner' da butona basınca rastgele bir hızda dönecek şekilde aralık, yavaşlama hızı, animation curve gibi değerler belirledim.
- Bu şekilde devam ederek çarktan kazanılan ödülü ekranın solundaki panele yerleşecek şekilde kurguladım ve DoTween ile text ve image animasyonu yarattım.
- Daha sonra bombanın denk gelmediği bölgeler için bölge sayısını arttırdım ve bombanın geldiği bölgeler için ise mevcut bölgeyi default değere geri aldım.
- Bonus özellik için bir kurgulama yapmadım ve bomba ile karşılaşılan durumlar için oyundaki değerleri sıfırladım ve oyuncuya herhangi bir ödül vermeden, biriktirdiği ödülleri sıfırladım.
- Oyuncu çark dönmediği sürece her bölgede oyunu terkedebilecek şekilde kurguladım ve çark dönerken "EXIT" butonunun interactable özelliğini kapattım.
- Oyuncu "EXIT" butonuna bastığında ödül toplama veya devam etme seçeneğini görebilmesi için panel aktif ettim. Ancak ödül toplama butonu ile ilgili methodları yetiştiremedim.(Sadece console' a log atacak şekilde bıraktım.)
- Hata olasılığını azaltmak için buttonlar, yaratılan öğeler, isimleri, script referansları gibi değerleri OnValidate ile atama yapmaya gayret ettim.
- Ekranın üst kısmında, oyunun devam etme veya bitme durumuna göre objelerin arka planları, renkleri gibi durumlarını yönettim.
- Ekranın sağ kısmında, oyuncunun en yakın olduğu süper bölge ve güvenli bölgenin numaralarını yazdırdım.
- Oyun içerisinde spritelar için Sprite Atlas kullandım. %35 oranında bir iyileşme ile karşılaştım.
- SourceTree kullanarak değişikliklerimi pushladım ve semantic commit mesajları kullanımına dikkat ettim.
- Yorum satırları ile daha okunaklı bir hale getirdim.

#
Projeden anladıklarım, oluşturduğum içerik ve içeriği oluştururken izlediğim adımlar hakkında kısaca bahsedebileceklerim bunlar.
