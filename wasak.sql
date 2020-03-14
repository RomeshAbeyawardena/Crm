DECLARE @hashes HASH
INSERT INTO @hashes
	SELECT 0x6336623564 UNION SELECT
                0x6662633461 UNION SELECT
                0x6331346632

SELECT DISTINCT([Customer].[Id]), [dbo].[Customer].[EmailAddress],
		[dbo].[Customer].[FirstName], [dbo].[Customer].[MiddleName],
		[dbo].[Customer].[LastName], [dbo].[Customer].[Password],
		[dbo].[Customer].[Active], [dbo].[Customer].[Created],
		[dbo].[Customer].[Modified],[dbo].[Customer].[LastIndexed] 
	FROM @hashes h
	INNER JOIN [dbo].[CustomerHash]
	ON [dbo].[CustomerHash].[Hash] = h.[Value]
	INNER JOIN [dbo].[Customer]
	ON [Customer].[Id] = [CustomerHash].[CustomerId]