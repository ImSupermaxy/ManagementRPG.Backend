CREATE OR REPLACE FUNCTION sp001getlast()
RETURNS TABLE(id INT, nome VARCHAR, versao VARCHAR, status SMALLINT, userinsid int, usermodid int, userinsdata TIMESTAMP, usermoddata TIMESTAMP)
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT 
		MAX(t001.id) as id, 
		t001.nome as nome,
		t001.versao as versao,
		t001.status as status,
		t001.userinsid as userinsid,
		t001.usermodid as usermodid,
		t001.userinsdata as userinsdata,
		t001.usermoddata as usermoddata
    FROM tbl_001_sistema t001
    WHERE t001.status = 1
	GROUP BY t001.id
	ORDER BY t001.userinsdata desc;
END;
$$;

SELECT * FROM sp001getlast();



