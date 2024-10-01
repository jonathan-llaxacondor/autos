CREATE TABLE MARCA (
    ID INT IDENTITY PRIMARY KEY,
    NOMBRE VARCHAR(100) NOT NULL,
    ESTADO BIT NOT NULL DEFAULT 1,
    FECHA_CREACION DATETIME DEFAULT GETDATE()
);

CREATE TABLE TIPO_AUTO (
    ID INT IDENTITY PRIMARY KEY,
    NOMBRE VARCHAR(100) NOT NULL,
    ESTADO BIT NOT NULL DEFAULT 1,
    FECHA_CREACION DATETIME DEFAULT GETDATE()
);


CREATE TABLE AUTO (
    ID INT IDENTITY PRIMARY KEY,
    IDMARCA INT NOT NULL,
    IDTIPOAUTO INT NOT NULL,
    MODELO VARCHAR(50) NOT NULL,
    ANIO VARCHAR(10) NOT NULL,
    COLOR VARCHAR(30) NOT NULL,
    PRECIO DECIMAL(10, 2) NOT NULL,
    KILOMETRAJE DECIMAL(10,2) NOT NULL,
    FECHA_CREACION DATETIME NOT NULL DEFAULT GETDATE(),
    FECHA_MODIFICACION DATETIME NULL,
    ESTADO BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (IDMARCA) REFERENCES MARCA(ID),
    FOREIGN KEY (IDTIPOAUTO) REFERENCES TIPO_AUTO(ID)
);




insert into MARCA
(nombre)
values
('Toyota'),
('Ford'),
('Chevrolet'),
('Honda'),
('Nissan');

insert into TIPO_AUTO
(nombre)
values
('Sedán'),
('SUV'),
('Camioneta'),
('Hatchback'),
('Coupé');


CREATE OR ALTER PROC SP_GET_LIST_AUTO
AS
BEGIN
	SET NOCOUNT ON; 
	SELECT
		A.ID,
		M.NOMBRE MARCA,
		TA.NOMBRE TIPOAUTO,
		A.MODELO,
		A.ANIO,
		A.COLOR,
		A.PRECIO,
		A.KILOMETRAJE,
		A.ESTADO
	FROM AUTO (NOLOCK) A
	INNER JOIN TIPO_AUTO TA ON A.IDTIPOAUTO = TA.ID
	INNER JOIN MARCA M ON A.IDMARCA = M.ID
	WHERE A.ESTADO = 1;
END;

CREATE OR ALTER PROC SP_GET_LIST_COMBO_TIPOAUTO
AS
BEGIN
	SET NOCOUNT ON; 
	SELECT
		ID,
		NOMBRE DESCRIPCION
	FROM TIPO_AUTO (NOLOCK)
	WHERE ESTADO = 1;
END;


CREATE OR ALTER PROC SP_CU_AUTO
	@ID INT NULL = NULL,
	@IDMARCA INT,
	@IDTIPOAUTO INT,
	@MODELO	VARCHAR(50),	
	@ANIO VARCHAR(10),
	@COLOR VARCHAR(30),
	@PRECIO DECIMAL(10, 2),
	@KILOMETRAJE DECIMAL(10, 2)
AS
BEGIN

	DECLARE @ok BIT, @mensaje NVARCHAR(MAX);

	BEGIN TRY 
		
		IF @ID IS NULL
		BEGIN

			BEGIN TRAN;
		 
				INSERT INTO AUTO
				(
					IDMARCA, IDTIPOAUTO, MODELO, ANIO, COLOR, PRECIO, KILOMETRAJE, FECHA_CREACION
				)
				VALUES
				(
					@IDMARCA, @IDTIPOAUTO, @MODELO, @ANIO, @COLOR, @PRECIO, @KILOMETRAJE, GETDATE()
				);

			SET @ok = 1;
			SET @mensaje = 'El auto se registró satisfactoriamente.';

			GOTO FINAL;

		END

		IF NOT EXISTS(SELECT TOP 1 1 FROM AUTO (NOLOCK) WHERE ID = @ID)
		BEGIN 
			SET @ok = 0;
			SET @mensaje = 'El auto [' + CAST(@ID AS VARCHAR(20)) + '] no existe.';
		END

		BEGIN TRAN;

			 UPDATE AUTO
			 SET  IDMARCA = @IDMARCA,			 
				  IDTIPOAUTO = @IDTIPOAUTO,
				  MODELO = @MODELO,
				  ANIO = @ANIO,
				  COLOR = @COLOR,
				  PRECIO = @PRECIO,
				  KILOMETRAJE = @KILOMETRAJE,
				  FECHA_MODIFICACION = GETDATE()
			 WHERE ID = @ID

			 SET @ok = 1;
			 SET @mensaje = 'El auto se actualizó satisfactoriamente.';

		GOTO FINAL;

		FINAL:
			SELECT @ok Ok, @mensaje Mensaje;
			IF @@TRANCOUNT > 0
				COMMIT TRAN;
	END TRY
	BEGIN CATCH
		IF @@trancount > 0
			ROLLBACK TRAN;
  
		SELECT
			0 Ok, 'Error: '+ CAST(ERROR_MESSAGE() AS VARCHAR) + N'SP: [ SP_CU_AUTO ] 2024-10-01 ' +
			N'. NroError: ' + CAST(ERROR_NUMBER() AS VARCHAR) + N'. Línea: ' + CAST(ERROR_LINE() AS VARCHAR) Mensaje; 
	END CATCH
END;



CREATE OR ALTER PROC SP_GET_AUTO_BY_ID
	@ID INT
AS
BEGIN
	SET NOCOUNT ON; 
	SELECT
		A.ID,
		A.MODELO,
		A.IDMARCA,
		A.IDTIPOAUTO,
		A.ANIO,
		A.COLOR,
		A.PRECIO,
		A.KILOMETRAJE,
		A.ESTADO
	FROM AUTO A (NOLOCK)
	WHERE ID = @ID;
END;


CREATE OR ALTER PROC SP_DISABLE_AUTO
	@ID INT
AS
BEGIN

	DECLARE @ok BIT, @mensaje NVARCHAR(MAX);

	BEGIN TRY 
		
		BEGIN TRAN;

			 UPDATE AUTO
			 SET  ESTADO = 0,
				  FECHA_MODIFICACION = GETDATE()
			 WHERE ID = @ID

			 SET @ok = 1;
			 SET @mensaje = 'El auto se dio de baja satisfactoriamente.';

		GOTO FINAL;

		FINAL:
			SELECT @ok Ok, @mensaje Mensaje;
			IF @@TRANCOUNT > 0
				COMMIT TRAN;
	END TRY
	BEGIN CATCH
		IF @@trancount > 0
			ROLLBACK TRAN;
  
		SELECT
			0 Ok, 'Error: '+ CAST(ERROR_MESSAGE() AS VARCHAR) + N'SP: [ SP_DISABLE_AUTO ] 2024-10-01 ' +
			N'. NroError: ' + CAST(ERROR_NUMBER() AS VARCHAR) + N'. Línea: ' + CAST(ERROR_LINE() AS VARCHAR) Mensaje; 
	END CATCH
END;