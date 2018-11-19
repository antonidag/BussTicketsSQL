
\set ON_ERROR_STOP on

\c ae4864

DROP DATABASE IF EXISTS ae4864buss;
CREATE DATABASE ae4864buss;

\c ae4864buss

create table resenar
(
	email text primary key,
	namn text,
	telenr text,
	adress text
);
create table stad
(
	id serial primary key ,
	namn text,
	land text,
	adress text
);

create table buss
(
	regnr serial primary key,
	antalplatser integer
);

create table chaufor
(
	pnr integer primary key,
	namn text,
	telenr text,
	adress text
);

create table tur
(
	id serial primary key,
	fstad integer,
	tstad integer,
	datum date,
	avgang time,
	ankomst time,
	bussreg integer,
	chauforid integer,
	pris integer,
	
	foreign key (fstad) references stad(id),
	foreign key (tstad) references stad(id),
	foreign key (bussreg) references buss(regnr),
	foreign key (chauforid) references chaufor(pnr)
);


create table biljett
(
	id serial primary key,
	resenarid text,
	turid integer,
	antalbiljetter integer,
	
	foreign key (turid) references tur(id),
	foreign key (resenarid) references resenar(email)
	
);


insert into chaufor(pnr,namn,telenr,adress) values 
(
	940624,'Karl Jonsson','1111111','Jägar Vägen 6 Malmö'
),
(
	880736,'Klara Eriksson','222222','Stor Gaten 8 Lund'
),
(
	780346,'Alex Briksson','223242','Lilla Gaten 37 Lund'
),
(
	950714,'Johan Glad','3333333','Mellan Gaten 40 Malmö'
),
(
	880723,'Greta Larsson','792133','Circel Stigen 17 Malmö'
);

insert into buss(antalplatser) values 
(20),(30),(25),(40),(14),(35);

insert into resenar(email,namn,telenr,adress) values 
('antonidag@hotmail.com','Anton Björkman','079481345','Flädie banväg 6');

insert into stad(namn,land,adress) values 
(
	'Malmö','Sverige','Malmö C'
),
(
	'Lund','Sverige','Lund C'
),
(
	'Köpenhamn','Dannmark','Köpenhamn C'
),
(
	'Madrid','Spanien','Madrid C'
),
(
	'Berlin','Tyskland','Berlin C'
),
(
	'Budapest','Ungern','Budapest C'
),
(
	'Barcelona','Spanien','Barcelona C'
),
(
	'Munchen','Tysklan', 'Munchen'
),
(
	'Milano','Italien','Milano C'
),
(
	'Paris','Frankrike','Paris C'
),
(
	'Prag','Tjeckien', 'Prag C'
);

insert into tur(fstad,tstad,datum,avgang,ankomst,bussreg,chauforid,pris) values 
(
	1,3,'2017-07-01','17:00','18:00',1,940624,100
),
(
	3,11,'2017-07-03','15:00','21:00',4,940624,400
),
(
	2,3,'2017-07-17','13:00','14:10',5,880736,120
),
(
	3,7,'2017-07-08','9:00','21:00',2,940624,500
),
(
	1,6,'2017-07-15','8:00','17:00',3,880736,600
),
(
	3,1,'2017-07-05','15:00','16:00',6,780346,100
),
(
	11,3,'2017-07-06','15:00','21:00',2,880723,400
),
(
	1,10,'2017-07-23','10:00','17:00',4,950714,475
),
(
	4,8,'2017-07-12','9:00','15:00',1,880736,525
),
(
	6,10,'2017-08-12','13:00','20:00',6,940624,550
),
(
	11,10,'2017-08-01','10:00','16:00',4,780346,660
),
(
	7,2,'2017-07-25','10:00','18:00',3,880723,700
),
(
	4,1,'2017-07-07','9:00','15:00',5,780346,555
),
(
	10,2,'2017-07-26','10:00','17:00',4,940624,655
);






