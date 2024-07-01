
CREATE TABLE Ofertas (
    OfertaID INT PRIMARY KEY IDENTITY,
    ProductoID INT NOT NULL,
    NombreOfertante VARCHAR(100) NOT NULL,
    TelefonoOfertante VARCHAR(15) NOT NULL,
    MontoOferta DECIMAL(18, 2) NOT NULL,
    FechaOferta DATETIME2 NOT NULL,
    EstadoRegistro BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID)
);

