select * from tbl_002_usuario;

select * from tbl_001_sistema;

-- select * from tbl_001_sistema t001 inner join tbl_002_usuario t002 on t002.id = t001.userinsid;


-- PROCEDURES Para Insert / Update / Delete / Patch / TUDO menos get..
CREATE OR REPLACE PROCEDURE inserir_cliente(p_nome VARCHAR, p_email VARCHAR)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO clientes (nome, email) VALUES (p_nome, p_email);
END;
$$;

call spteste();

-- FUNCTIONS Para Get
CREATE OR REPLACE FUNCTION buscar_clientes(p_nome VARCHAR)
RETURNS TABLE(id INT, nome VARCHAR, email VARCHAR)
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT id, nome, email
    FROM clientes
    WHERE nome ILIKE '%' || p_nome || '%';
END;
$$;



