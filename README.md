# EntityFramework
Projeto simples no terminal usando EntityFramework

Pegue o arquivo MuitoParaMuitos.bak e importe na sua base de dados.

Vá até App.config e cole dentro da seção de config (não se esqueça de modificar com o nome do seu servidor e tipo):

<connectionStrings>
      <!--PROD-->
      <add name="ModeloEntidades" connectionString="data source=SEUSERVIDOR/TIPO;initial catalog=MuitosParaMuitos;persist security info=True;user id=SA;password=12345;" providerName="System.Data.SqlClient" />
  </connectionStrings>
