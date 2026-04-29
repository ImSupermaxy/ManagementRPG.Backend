CREATE OR REPLACE function sp002update(
	p_nome varchar(200), 
	p_email varchar(180), 
	p_arroba varchar(120),
	p_senhahash varchar(90),
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
	UPDATE tbl_002_usuario
	SET nome 			= p_nome,
		arroba			= p_arroba,
		senhahash		= p_senhahash,
	    -- email 			= p_email,
		status 			= p_status,
		usermodid 		= p_usermodid,
		usermoddata 	= p_usermoddata
	WHERE id 			= p_id;

 	GET DIAGNOSTICS crows = ROW_COUNT;

	RETURN crows;
END;
$$;