CREATE TABLE [dbo].[Incoming]
(
	[Id] INT Identity(1,1) NOT NULL PRIMARY KEY,
	MedicamentId int not null,
	Count int not null,
	Price decimal not null,
	IncomedAt DateTime not null, 
    CONSTRAINT [FK_Incoming_Medicament] FOREIGN KEY (MedicamentId) REFERENCES Medicament(MedicamentId)
)
