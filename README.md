<!DOCTYPE html>
<html lang="tr">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>A Sekiz Taş Bulmaca Çözücü - A* Algoritması</title>
</head>
<body>
    <h1>A Sekiz Taş Bulmaca Çözücü - A* Algoritması</h1>
    <p>Bu proje, C# ile geliştirilen bir <strong>8 Taş (8 Puzzle)</strong> bulmacayı A* algoritması kullanarak çözen bir uygulamadır.</p>
    <h2>📌 Proje Hakkında</h2>
    <p>8 Taş Bulmacası, 3x3 boyutunda bir tahta üzerinde 1’den 8’e kadar numaralanmış taşların ve bir boşluğun (0) bulunduğu klasik bir yapboz oyunudur. Amaç, taşları hareket ettirerek başlangıç durumundan hedef duruma en kısa adımda ulaşmaktır.</p>
    <h2>🧠 Kullanılan Algoritma</h2>
    <p>A* algoritması, <code>f(n) = g(n) + h(n)</code> maliyet fonksiyonuyla en kısa çözüm yolunu bulur:</p>
    <ul>
        <li><code>g(n)</code>: Başlangıçtan mevcut duruma kadar olan gerçek maliyet (adım sayısı)</li>
        <li><code>h(n)</code>: Hedefe kalan tahmini maliyet (Manhattan veya Hamming uzaklığı)</li>
    </ul>
    <h2>🛠️ Teknolojiler</h2>
    <ul>
        <li>C# (.NET Framework / .NET Core)</li>
        <li>Windows Forms (isteğe bağlı)</li>
        <li>OOP (Nesne Tabanlı Programlama)</li>
    </ul>
    <h2>🚀 Kurulum</h2>
    <ol>
        <li>Projeyi klonlayın:
            <pre><code>git clone https://github.com/Haknozer/A-sekiztas.git</code></pre>
        </li>
        <li>Visual Studio ile açın.</li>
        <li><code>Program.cs</code> dosyasını çalıştırarak uygulamayı başlatın.</li>
    </ol>
    <h2>✅ Özellikler</h2>
    <ul>
        <li>Rastgele başlangıç durumu girme</li>
        <li>Adım adım çözüm yolu gösterimi</li>
        <li>Çözüm süresi ve toplam adım sayısı bilgisi</li>
        <li>Çözülemeyen durumlarda kullanıcı bilgilendirmesi</li>
    </ul>
    <h2>👨‍💻 Geliştirici</h2>
    <p><strong>Hakan Özer</strong> – <a href="https://github.com/Haknozer" target="_blank" rel="noopener">GitHub</a></p>
</body>
</html>
