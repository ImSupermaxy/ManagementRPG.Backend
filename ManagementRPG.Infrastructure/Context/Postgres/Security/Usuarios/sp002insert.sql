CREATE OR REPLACE function sp001insert(
	p_nome TEXT, 
	p_versao TEXT, 
	p_status INT,
	p_userinsid INT, 
	p_usermodid INT, 
	p_userinsdata TIMESTAMP, 
	p_usermoddata TIMESTAMP
)
RETURNS INT
LANGUAGE plpgsql
AS $$
DECLARE
    novo_id INT;
BEGIN
    INSERT INTO tbl_001_sistema (nome, versao, status, userinsid, usermodid, userinsdata, usermoddata) 
	VALUES (p_nome, p_versao, p_status, p_userinsid, p_usermodid, p_userinsdata, p_usermoddata)

	RETURNING id INTO novo_id;

    RETURN novo_id;
END;
$$;


