namespace Diplom.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Diplom.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Diplom.ApplicationContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //IList<Departament> departaments = new List<Departament>();

            //departaments.Add(new Departament() { Name = "�����" });
            //foreach (Departament dep in departaments)
            //    context.Departaments.AddOrUpdate(dep);

            context.Departaments.AddOrUpdate(
                p => p.Name,
                new Departament { Name = "�����", ID = 1 }
                );
            context.Departaments.AddOrUpdate(
                 p => p.Name,
                 new Departament { Name = "�������������", ID = 2 }
                 );

            IList<ArtistCategory> artistCategorys = new List<ArtistCategory>();
            artistCategorys.Add(new ArtistCategory()
            {
                ID = 1,
                Name = "������"
            });
            artistCategorys.Add(new ArtistCategory()
            {
                ID = 2,
                Name = "������"
            });
            artistCategorys.Add(new ArtistCategory()
            {
                ID = 3,
                Name = "������"
            });
            artistCategorys.Add(new ArtistCategory()
            {
                ID = 4,
                Name = "�� ������"
            });
            foreach (ArtistCategory artCat in artistCategorys)
                context.ArtistCategorys.AddOrUpdate(artCat);

            IList<ContractType> contractTypes = new List<ContractType>();
            contractTypes.Add(new ContractType()
            {
                ID = 1,
                Name = "�����������"
            });
            contractTypes.Add(new ContractType()
            {
                ID = 2,
                Name = "���������"
            });
            foreach (ContractType conType in contractTypes)
                context.ContractTypes.AddOrUpdate(conType);

            IList<ConcertType> concertTypes = new List<ConcertType>();
            concertTypes.Add(new ConcertType()
            {
                ID = 1,
                Name = "�������"
            });
            concertTypes.Add(new ConcertType()
            {
                ID = 2,
                Name = "�������"
            });
            foreach (ConcertType conType in concertTypes)
                context.ConcertTypes.AddOrUpdate(conType);

            IList<ConcertPlaceType> concertPlaceTypes = new List<ConcertPlaceType>();
            concertPlaceTypes.Add(new ConcertPlaceType()
            {
                ID = 1,
                Name = "��������"
            });
            concertPlaceTypes.Add(new ConcertPlaceType()
            {
                ID = 2,
                Name = "��������"
            });
            concertPlaceTypes.Add(new ConcertPlaceType()
            {
                ID = 3,
                Name = "�����������"
            });
            foreach (ConcertPlaceType conPlaceType in concertPlaceTypes)
                context.ConcertPlaceTypes.AddOrUpdate(conPlaceType);


            IList<Worker> workers = new List<Worker>();

            workers.Add(new Worker()
            {
                Name = "������",
                Surname = "�����",
                Patronymic = "��������",
                RegistrationPlace = "�����",
                BirthPlace = "�����",
                DateOfBirth = new DateTime(1975, 10, 9, 0, 0, 0),
                Nationality = "������",
                Education = "������",
                Sex = true,
                MaritalStatus = false,
                PhoneNum = "4444444",
                Email = "mista@mail.ru",
                DepartamentID = 2,
                ID = 3
            }
            );
            workers.Add(new Worker()
            {
                Name = "������",
                Surname = "��������",
                Patronymic = "���������",
                RegistrationPlace = "�����",
                BirthPlace = "�����",
                DateOfBirth = new DateTime(1985, 10, 9, 0, 0, 0),
                Nationality = "�������",
                Education = "������",
                Sex = true,
                MaritalStatus = true,
                PhoneNum = "2022327",
                Email = "soska@mail.ru",
                DepartamentID = 1,
                ID = 1
            }
            );
            workers.Add(new Worker()
            {
                Name = "������",
                Surname = "�������",
                Patronymic = "�����������",
                RegistrationPlace = "�����",
                BirthPlace = "����",
                DateOfBirth = new DateTime(1966, 6, 6, 0, 0, 0),
                Nationality = "�������",
                Education = "������",
                Sex = true,
                MaritalStatus = false,
                PhoneNum = "503819",
                Email = "klon_klopa@gmail.com",
                DepartamentID = 1,
                ID = 2
            });
            workers.Add(new Worker()
            {
                Name = "�����",
                Surname = "����",
                Patronymic = "�����������",
                RegistrationPlace = "�����",
                BirthPlace = "������",
                DateOfBirth = new DateTime(1988, 11, 25, 0, 0, 0),
                Nationality = "�������",
                Education = "������",
                Sex = true,
                MaritalStatus = false,
                PhoneNum = "666123",
                Email = "Globeshar@gmail.com",
                DepartamentID = 1,
                ID = 4
            });
            workers.Add(new Worker()
            {
                Name = "����",
                Surname = "���������",
                Patronymic = "���������",
                RegistrationPlace = "�����",
                BirthPlace = "�����",
                DateOfBirth = new DateTime(1955, 1, 11, 0, 0, 0),
                Nationality = "�������",
                Education = "������",
                Sex = true,
                MaritalStatus = true,
                PhoneNum = "898956",
                Email = "keinthebanana@mail.com",
                DepartamentID = 2,
                ID = 5
            });
            workers.Add(new Worker()
            {
                Name = "���������",
                Surname = "��������",
                Patronymic = "��������",
                RegistrationPlace = "�����",
                BirthPlace = "��������",
                DateOfBirth = new DateTime(1990, 2, 3, 0, 0, 0),
                Nationality = "�������",
                Education = "������",
                Sex = false,
                MaritalStatus = false,
                PhoneNum = "209901",
                Email = "GrayMizari@gmail.com",
                DepartamentID = 1,
                ID = 6
            });
            workers.Add(new Worker()
            {
                Name = "������",
                Surname = "����������",
                Patronymic = "�������������",
                RegistrationPlace = "�����",
                BirthPlace = "�����",
                DateOfBirth = new DateTime(1980, 5, 4, 0, 0, 0),
                Nationality = "�������",
                Education = "������",
                Sex = true,
                MaritalStatus = false,
                PhoneNum = "111222",
                Email = "Andreader@gmail.com",
                DepartamentID = 1,
                ID = 7
            });
            foreach (Worker wrk in workers)
                context.Workers.AddOrUpdate(wrk);



            IList<Contract> contracts = new List<Contract>();

            contracts.Add(new Contract()
            {
                ID = 6,
                ContractTypeId = 1,
                ArtistCategoryId = 3,
                WorkerId = 4,
                BeginningDate = new DateTime(2017, 4, 12, 0, 0, 0),
                EndDate = new DateTime(2019, 4, 12, 0, 0, 0),
                OrderNum = 1116,
                Position = "���������"
            });
            contracts.Add(new Contract()
            {
                ID = 7,
                ContractTypeId = 1,
                ArtistCategoryId = 4,
                WorkerId = 5,
                BeginningDate = new DateTime(2017, 2, 12, 0, 0, 0),
                EndDate = new DateTime(2019, 2, 12, 0, 0, 0),
                OrderNum = 1117,
                Position = "���������"
            });
            contracts.Add(new Contract()
            {
                ID = 8,
                ContractTypeId = 1,
                ArtistCategoryId = 2,
                WorkerId = 6,
                BeginningDate = new DateTime(2017, 1, 12, 0, 0, 0),
                EndDate = new DateTime(2019, 1, 12, 0, 0, 0),
                OrderNum = 1118,
                Position = "���������"
            });
            contracts.Add(new Contract()
            {
                ID = 9,
                ContractTypeId = 1,
                ArtistCategoryId = 4,
                WorkerId = 7,
                BeginningDate = new DateTime(2017, 4, 20, 0, 0, 0),
                EndDate = new DateTime(2019, 4, 20, 0, 0, 0),
                OrderNum = 1119,
                Position = "���������"
            });
            contracts.Add(new Contract()
            {
                ID = 5,
                ContractTypeId = 1,
                ArtistCategoryId = 4,
                WorkerId = 3,
                BeginningDate = new DateTime(2017, 4, 12, 0, 0, 0),
                EndDate = new DateTime(2019, 4, 12, 0, 0, 0),
                OrderNum = 1115,
                Position = "���������"
            });
            contracts.Add(new Contract()
            {
                ID = 1,
                ContractTypeId = 1,
                ArtistCategoryId = 1,
                WorkerId = 1,
                BeginningDate = new DateTime(2013, 4, 12, 0, 0, 0),
                EndDate = new DateTime(2015, 4, 12, 0, 0, 0),
                OrderNum = 1111,
                Position = "���������"
            });
            contracts.Add(new Contract()
            {
                ID = 2,
                ContractTypeId = 2,
                ArtistCategoryId = 2,
                WorkerId = 1,
                BeginningDate = new DateTime(2015, 4, 12, 0, 0, 0),
                EndDate = new DateTime(2017, 4, 12, 0, 0, 0),
                OrderNum = 1112,
                Position = "���������"
            });
            contracts.Add(new Contract()
            {
                ID = 3,
                ContractTypeId = 1,
                ArtistCategoryId = 3,
                WorkerId = 1,
                BeginningDate = new DateTime(2017, 4, 12, 0, 0, 0),
                EndDate = new DateTime(2019, 4, 12, 0, 0, 0),
                OrderNum = 1113,
                Position = "���������"
            });
            contracts.Add(new Contract()
            {
                ID = 4,
                ContractTypeId = 1,
                ArtistCategoryId = 3,
                WorkerId = 2,
                BeginningDate = new DateTime(2016, 5, 20, 0, 0, 0),
                EndDate = new DateTime(2018, 5, 20, 0, 0, 0),
                OrderNum = 1114,
                Position = "���������"
            });
            foreach (Contract cont in contracts)
                context.Contracts.AddOrUpdate(cont);



            IList<ConcertEvent> concert = new List<ConcertEvent>();
            IList<ConcertMarks> concertMark = new List<ConcertMarks>();

            concert.Add(new ConcertEvent()
            {
                ID = 3,
                BeginningDate = new DateTime(2015, 4, 12, 0, 0, 0),
                EndDate = new DateTime(2015, 5, 12, 0, 0, 0),
                Country = "�������",
                City = "����",
                Address = "������",
                ConcertTypeId = 2,
                ConcertPlaceTypeId = 3,
                Description = "������� � �������"
            });
            concert.Add(new ConcertEvent()
            {
                ID = 4,
                BeginningDate = new DateTime(2017, 2, 20, 0, 0, 0),
                EndDate = new DateTime(2017, 3, 5, 0, 0, 0),
                Country = "�����",
                City = "�����",
                Address = "��. ��� ��� ���� 8",
                ConcertTypeId = 1,
                ConcertPlaceTypeId = 3,
                Description = "��� �������� �������� � �����"
            });
            concert.Add(new ConcertEvent()
            {
                ID = 5,
                BeginningDate = new DateTime(2016, 7, 22, 0, 0, 0),
                EndDate = new DateTime(2016, 7, 30, 0, 0, 0),
                Country = "������",
                City = "�����",
                Address = "��. ���� 12",
                ConcertTypeId = 2,
                ConcertPlaceTypeId = 3,
                Description = "������� � ��������"
            });
            concert.Add(new ConcertEvent()
            {
                ID = 6,
                BeginningDate = new DateTime(2014, 4, 12, 0, 0, 0),
                EndDate = new DateTime(2014, 5, 12, 0, 0, 0),
                Country = "������",
                City = "������",
                Address = "��. ����� ������ 7",
                ConcertTypeId = 1,
                ConcertPlaceTypeId = 3,
                Description = "��� �������� �������� � ������"
            });
            concertMark.Add(new ConcertMarks()
            {
                ID = 4,
                NumOfMarks = 60,
                ConcertEventID = 3,
                WorkerID = 1
            });
            concertMark.Add(new ConcertMarks()
            {
                ID = 5,
                NumOfMarks = 55,
                ConcertEventID = 3,
                WorkerID = 2
            });
            concert.Add(new ConcertEvent()
            {
                ID = 1,
                BeginningDate = new DateTime(2017, 4, 12, 0, 0, 0),
                EndDate = new DateTime(2017, 5, 12, 0, 0, 0),
                Country = "������",
                City = "������",
                Address = "��. ����� ������ 7",
                ConcertTypeId = 1,
                ConcertPlaceTypeId = 3,
                Description = "��� �������� �������� � ������"
            });
            concertMark.Add(new ConcertMarks()
            {
                ID = 1,
                NumOfMarks = 80,
                ConcertEventID = 1,
                WorkerID = 1
            });
            concertMark.Add(new ConcertMarks()
            {
                ID = 2,
                NumOfMarks = 60,
                ConcertEventID = 1,
                WorkerID = 2
            });
            concert.Add(new ConcertEvent()
            {
                ID = 2,
                BeginningDate = new DateTime(2016, 9, 12, 0, 0, 0),
                EndDate = new DateTime(2016, 9, 20, 0, 0, 0),
                Country = "������",
                City = "������",
                Address = "��. ����������� 12",
                ConcertTypeId = 2,
                ConcertPlaceTypeId = 3,
                Description = "������ � ��� ������"
            });
            concertMark.Add(new ConcertMarks()
            {
                ID = 3,
                NumOfMarks = 25,
                ConcertEventID = 2,
                WorkerID = 1
            });

            foreach (ConcertEvent conc in concert)
                context.ConcertEvents.AddOrUpdate(conc);
            foreach (ConcertMarks concMark in concertMark)
                context.ConcertMarks.AddOrUpdate(concMark);
            base.Seed(context);

        }
    }
}
