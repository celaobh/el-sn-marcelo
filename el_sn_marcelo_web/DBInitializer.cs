using el_sn_marcelo_api_cadastro_infrastructure.Adapter.Database;


namespace el_sn_marcelo_web
{

    public class DBInitializer
    {
        private DapperConnection _dapper;

        public DBInitializer()
        {
            _dapper = new DapperConnection();
        }

        public void CriarTabelas()
        {
            _dapper.Execute(@"
                            CREATE TABLE usuario
                            (id INTEGER PRIMARY KEY,
                            nome TEXT NOT NULL,
                            cpf TEXT NULL,
                            matricula INTEGER NULL,
                            aniversario TEXT NOT NULL,
                            cep TEXT NOT NULL,
                            logradouro TEXT NOT NULL,
                            numero int NOT NULL,
                            complemento TEXT NULL,
                            cidade TEXT NOT NULL,
                            estado TEXT NOT NULL); ");

            _dapper.Execute(@"CREATE TABLE veiculo
                            (id int NOT NULL PRIMARY KEY,
                            placa varchar NOT NULL,
                            id_marca int NOT NULL,
                            id_modelo int NOT NULL,
                            ano int NOT NULL,
                            valor_hora decimal(10,2) NOT NULL,
                            combustivel int NOT NULL,
                            limite_porta_malas int NOT NULL,
                            categoria int NOT NULL);");
        }

        public void InserirDados()
        {
            _dapper.Execute($@"INSERT INTO usuario (nome, cpf, aniversario, cep, logradouro, numero, complemento, cidade, estado) VALUES ('Marcelo', '00000011136', '{System.DateTime.Now.ToString()}', '30000000', 'Rua Obsoleta', 100, 'A', 'Belo Horizonte', 'MG')");
        }
    }
}
