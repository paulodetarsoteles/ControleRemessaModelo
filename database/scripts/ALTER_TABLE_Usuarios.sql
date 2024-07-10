--Adição da coluna Roles_id
ALTER TABLE tb_Usuarios 
ADD COLUMN Roles_Id INTEGER
REFERENCES tb_Roles(Id);

--Atualizado o único usuário do banco
UPDATE tb_Usuarios 
SET Roles_Id = 1 
WHERE Id = 1;

--Retirada da coluna de Role da tabela de usuários
ALTER TABLE tb_Usuarios 
DROP COLUMN Role;
