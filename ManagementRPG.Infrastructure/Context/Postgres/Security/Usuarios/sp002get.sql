CREATE OR REPLACE FUNCTION sp002get(p_id int, p_nome varchar(200), p_email varchar(180), p_arroba varchar(120), p_status SMALLINT)
RETURNS TABLE(id INT, nome VARCHAR, email varchar, arroba varchar, senhahash varchar, status SMALLINT, userinsid int, usermodid int, userinsdata TIMESTAMP, usermoddata TIMESTAMP)
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT *
    FROM tbl_002_usuario t002
    WHERE t002.id 	= COALESCE(p_id, t002.id)
	AND t002.status = COALESCE(p_status, t002.status)
	AND t002.email 	= COALESCE(p_email, t002.email)
	AND t002.nome	= COALESCE(p_nome, t002.nome)
	AND t002.arroba	= COALESCE(p_arroba, t002.arroba);
END;
$$;

SELECT * FROM sp002get(null, null, null, null, null);