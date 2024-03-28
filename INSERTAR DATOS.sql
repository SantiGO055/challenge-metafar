USE [Challenge_Metafar]
GO


INSERT INTO [dbo].[Tarjeta]
           ([NroTarjeta]
           ,[Pin]
           ,[Intentos]
           ,[TarjetaBloqueada])
     VALUES
           (123456,1234,3,'false'),(879454,1111,3,'false'),(564123,4444,3,'false'),(879423,5647,3,'false')
GO



INSERT INTO [dbo].[TipoMovimiento]
           ([DescripcionMovimiento])
     VALUES
           ('Extraccion'), ('Consulta')
GO



INSERT INTO [dbo].[CuentaBancaria]
           ([IDMovimiento]
           ,[IDTarjeta]
           ,[NroCuenta]
           ,[Saldo])
     VALUES
           (1,1,234,20000.00), (2,2,456,50000.00),(3,3,789,25.30),(4,4,564,300.78)
GO






INSERT INTO [dbo].[Usuario]
           ([IDCuentaBancaria]
           ,[Nombre])
     VALUES
           (1, 'Pepito'), (2, 'Juan'),(3, 'Pablo'), (4, 'Sebastian')
GO





INSERT INTO [dbo].[Movimiento]
           ([IDCuentaBancaria]
           ,[IDTipoMovimiento]
		   ,[Saldo]
           ,[FechaMovimiento]
		   )
     VALUES
           (1
           ,2
		   ,20000.00
           ,'27/03/2024')
GO