CREATE OR REPLACE function sp001update(
	p_nome TEXT,
	p_versao TEXT,
	p_status INT,
	p_usermodid INT, 
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
	SET nome 			= p_nome,
	    versao 			= p_versao,
		status 			= p_status,
		usermodid 		= p_usermodid,
		usermoddata 	= p_usermoddata
	WHERE id 			= p_id;

 	GET DIAGNOSTICS crows = ROW_COUNT;

	RETURN crows;
END;
$$;