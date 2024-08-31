# SMD_READER_LIB
C-Sharp SMD Reader LIB (StudioModel Data)

**Translate from Portuguese Brazil**
<br>
<br>* Escrito em: C-Sharp
<br>* Licença: MIT Licence
<br>
<br>Código destinado para carregar arquivo .SMD (StudioModel Data) e converter para uma classe.
<br>Fiz esse código, pois não achei um que pudesse usar, então ele é o mais simples possível.
<br>Os códigos de teste unitário, é a primeira vez que faço, eles tem como objetivo verificar as situações de exception (arquivo fora do layout) e conferir se os valores estão sendo atribuídos corretamente nos campos.
<br>
<br> * O código principal está em "SMD_READER_LIB\SmdReader.cs", copie esse arquivo para o seu projeto ou gere um arquivo .dll.
<br> * Em "SmdReaderTest\UnitTestSmdReader.cs" são os testes unitários;
<br> * Em "test\Program.cs" é um exemplo de uso.

**Exemplo De Uso**

```
System.IO.StreamReader stream = new System.IO.FileInfo("arquivo.smd").OpenText();
SMD_READER_LIB.SMD smd = SMD_READER_LIB.SmdReader.Reader(stream);
```
Use a variável "smd" para obter o conteúdo do arquivo.
<br>Nota: você fica responsável por fechar o "stream", independente da leitura for bem sucedida ou não, o "stream" vai ficar aberto.

----
**By: JADERLINK**