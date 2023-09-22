# SMD_READER_API
C-Sharp SMD Reader API (StudioModel Data)

Translate from Portuguese Brazil

* Escrito em: C-Sharp
* Licença: MIT Licence

<br>Código destinado para carregar arquivo .SMD (StudioModel Data) e converter para uma classe.
<br>Fiz esse código, pois não achei um que pudesse usar, então ele é o mais simples possível.
<br>Os códigos de teste unitário, é a primeira vez que faço, eles tem como objetivo verificar as situações de exception (arquivo fora do layout), e conferir se os valores estão sendo atribuídos corretamente nos campos.

<br> * O código principal está em "SMD_READER_API\SmdReader.cs", copie esse arquivo para o seu projeto ou gere um arquivo .dll.
<br> * Em "SmdReaderTest\UnitTestSmdReader.cs" são os testes unitários;
<br> * Em "test\Program.cs" é um exemplo de uso.

**Exemplo De Uso**

```
System.IO.StreamReader stream = new System.IO.FileInfo("arquivo.smd").OpenText();
SMD_READER_API.SMD smd = SMD_READER_API.SmdReader.Reader(stream);
```
Use a variável "smd" para obter o conteúdo do arquivo.
<br>Nota: o método "Reader" fechar o stream se for bem sucedido, caso tiver exception, o stream não será fechado.

----
**By: JADERLINK**