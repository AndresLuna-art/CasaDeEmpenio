CREATE TABLE Devoluciones (
    DevolucionID INT PRIMARY KEY IDENTITY,
    ProductoID INT NOT NULL,
    FechaDevolucion DATETIME2 NOT NULL,
    EstadoRegistro BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID)
);
