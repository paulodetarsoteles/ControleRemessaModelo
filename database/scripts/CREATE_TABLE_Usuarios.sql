--Criar a tabela de usuários
CREATE TABLE tb_Usuarios(
Id INTEGER PRIMARY KEY AUTOINCREMENT  NOT NULL, 
Login VARCHAR(30), 
Senha VARCHAR(500), 
Nome VARCHAR(80), 
Role VARCHAR(40), 
Email VARCHAR(100), 
Telefone VARCHAR(11)
);