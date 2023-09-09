--SELECT 
--    c.name 'Column Name',
--    c.max_length 'Max Length',
--    c.precision ,
--    c.scale ,
--    c.is_nullable
--	--,ISNULL(i.is_primary_key, 0) 'Primary Key'
--FROM    
--    sys.columns c
--WHERE
--    c.object_id = OBJECT_ID('[dbo].[Employees]')

SELECT
	obj.name AS 'Table Name',
	obj.type_desc 'Table Type',
    c.name 'Column Name',
    t.Name 'Data type',
    c.max_length 'Max Length',
    c.precision ,
    c.scale ,
    c.is_nullable,
    ISNULL(i.is_primary_key, 0) 'Primary Key'
FROM    
    sys.columns c
INNER JOIN sys.types t ON c.user_type_id = t.user_type_id
INNER JOIN sys.objects obj on c.object_id = obj.object_id
LEFT OUTER JOIN sys.index_columns ic ON ic.object_id = c.object_id AND ic.column_id = c.column_id
LEFT OUTER JOIN sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id
WHERE obj.type_desc = 'USER_TABLE'
    --c.object_id IN (OBJECT_ID('Employees'), OBJECT_ID('Countries'))

--exec sp_columns Employees

--SELECT * FROM sys.columns order by name
--SELECT * FROM sys.objects order by type_desc desc