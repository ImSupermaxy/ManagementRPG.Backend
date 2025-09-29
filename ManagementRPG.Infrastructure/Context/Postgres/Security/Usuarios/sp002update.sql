CREATE OR REPLACE function sp001update(
	p_nome TEXT, 
	p_versao TEXT, 
	p_status INT, 
	p_userinsid INT, 
	p_usermodid INT, 
	p_userinsdata TIMESTAMP, 
	p_usermoddata TIMESTAMP,
	p_id INT
)
RETURNS INT
LANGUAGE plpgsql
AS $$
DECLARE
    crows INT;
BEGIN
	UPDATE tbl_001_sistema
	SET nome = p_nome,
	    versao = p_versao,
		status = p_status,
		userinsid = p_userinsid,
		usermodid = p_usermodid,
		userinsdata = p_userinsdata,
		usermoddata = p_usermoddata
	WHERE id = p_id;

 	GET DIAGNOSTICS crows = ROW_COUNT;

	RETURN crows;
END;
$$;