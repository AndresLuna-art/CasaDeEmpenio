CREATE TABLE Productos (
    ProductoID INT PRIMARY KEY IDENTITY,
    TipoProducto VARCHAR(50) NOT NULL,
    NombreProducto VARCHAR(100) NOT NULL,
    EstadoProducto VARCHAR(50) NOT NULL,
    FechaIngreso DATETIME2 NOT NULL,
    ValorCalculado DECIMAL(18, 2) NOT NULL,
    FechaVencimientoDevolucion DATETIME2 NOT NULL,
    EstadoRegistro BIT NOT NULL DEFAULT 1
);
