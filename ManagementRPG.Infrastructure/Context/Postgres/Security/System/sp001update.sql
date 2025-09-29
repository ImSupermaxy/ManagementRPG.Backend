CREATE OR REPLACE function sp001update(
	p_id INT,
	p_nome VARCHAR(45), 
	p_versao VARCHAR(24), 
	p_status SMALLINT, 
	p_userinsid INT, 
	p_usermodid INT, 
	p_userinsdata TIMESTAMP, 
	p_usermoddata TIMESTAMP
)
RETURNS INT
LANGUAGE plpgsql
AS $$
BEGIN
	UPDATE tbl_001_sistema
	SET nome = p_nome,
	    versao = p_versao,
		status = p_status,
		userinsid = p_userinsid,
		usermodid = p_usermodid,
		userinsdata = p_userinsdata,
		usermoddata = p_usermoddata
	WHERE id = p_id
	RETURNING COUNT(*);
END;
$$;