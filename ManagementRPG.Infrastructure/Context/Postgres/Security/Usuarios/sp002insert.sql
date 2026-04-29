CREATE OR REPLACE function sp002insert(
	p_nome varchar(200), 
	p_email varchar(180), 
	p_arroba varchar(120),
	p_senhahash varchar(90),
	p_status INT,
	p_userinsid INT,
	p_userinsdata TIMESTAMP
)
RETURNS INT
LANGUAGE plpgsql
AS $$
DECLARE
    novo_id INT;
BEGIN
    INSERT INTO tbl_002_usuario (nome, email, arroba, senhahash, status, userinsid, usermodid, userinsdata, usermoddata) 
	VALUES (p_nome, p_email, p_arroba, p_senhahash, p_status, p_userinsid, p_userinsid, p_userinsdata, p_userinsdata)
	
	RETURNING id INTO novo_id;

    RETURN novo_id;
END;
$$;
