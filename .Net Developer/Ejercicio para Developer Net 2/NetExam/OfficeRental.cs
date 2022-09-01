namespace NetExam
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NetExam.Abstractions;
    using NetExam.Dto;

    public class OfficeRental : IOfficeRental
    {

        //public static List<ILocation> IlistLocations = new List<ILocation>();
        public static List<LocationSpecs> listLocations = new List<LocationSpecs>();
        public static List<OfficeSpecs> listOffices = new List<OfficeSpecs>();
        public static List<BookingRequest> listBookingRequests = new List<BookingRequest>();
        

        public void AddLocation(LocationSpecs locationSpecs)
        {
            var locales = GetLocations();
            var existe = locales.Where(x => x.Name == locationSpecs.Name);
             
            if (existe.Count() != 0)
            {
                throw new Exception("Este local ya existe.");
            }
            else
            {
                listLocations.Add(locationSpecs);
            }
            
            
        }

        public void AddOffice(OfficeSpecs officeSpecs)
        {
            var locales = GetLocations();
            var existe = locales.Where(x => x.Name == officeSpecs.LocationName).ToList();

            if (existe.Count() != 0)
            {
                listOffices.Add(officeSpecs);
            }
            else
            {
                throw new Exception("El nombre de local para la oficina no existe.");
            }
           
            
            
        }

        public void BookOffice(BookingRequest bookingRequest)
        {
            //deberia pregu8ntar si esa oficina no esta reservada ese día
            var reservas = GetBookings(bookingRequest.LocationName, bookingRequest.OfficeName);
            var reservas2 = listBookingRequests.Where(x => x.LocationName == bookingRequest.LocationName && x.OfficeName == bookingRequest.OfficeName);
            DateTime inicioRangoHorarioReserva;
            DateTime finRangoHorarioReserva;

            DateTime inicioRangoRequest = bookingRequest.DateTime;
            DateTime finRangoHorarioRequest = bookingRequest.DateTime.AddHours(bookingRequest.Hours);

            foreach (var item in reservas2) //reserva por reserva
            {
                //me fijo el rango horario de la reserva
                inicioRangoHorarioReserva = item.DateTime;
                finRangoHorarioReserva = item.DateTime.AddHours(item.Hours);


                if(inicioRangoRequest <= finRangoHorarioReserva || finRangoHorarioRequest >= inicioRangoHorarioReserva)
                {
                    throw new Exception("La oficina ya esta reservada ese dia.");
                }
                else
                {
                    listBookingRequests.Add(bookingRequest);
                }


            }

            listBookingRequests.Add(bookingRequest);

        }

        public IEnumerable<IBooking> GetBookings(string locationName, string officeName)
        {
            List<BookingRequest> listBookingXLocationXOffice = new List<BookingRequest>();

            foreach (var item in listBookingRequests)
            {
                if(item.LocationName == locationName && item.OfficeName == officeName)
                    listBookingXLocationXOffice.Add(item);
            }

            return listBookingXLocationXOffice;
        }

        public IEnumerable<ILocation> GetLocations()
        {
            Console.WriteLine(listLocations);
            
            return listLocations;

        }

        public IEnumerable<IOffice> GetOffices(string locationName)
        {
            List<OfficeSpecs> listOfficesXLocation = new List<OfficeSpecs>();

            foreach (var item in listOffices)
            {
                if(item.LocationName == locationName)
                    listOfficesXLocation.Add(item);
            }

            return listOfficesXLocation;
            
        }

        public IEnumerable<IOffice> GetOfficeSuggestion(SuggestionRequest suggestionRequest)
        {
            
            var locales = listLocations.ToList();

            var oficinasConCapacidad = listOffices
                .Where(x => x.MaxCapacity >= suggestionRequest.CapacityNeeded) //filtro por capacidad
                .ToList();
            var oficinasConRecursos = new List<OfficeSpecs>();
           

                foreach (var myOffi in oficinasConCapacidad) //recorre oficina x oficina
                {
                    var coincidencias = myOffi.AvailableResources.Intersect(suggestionRequest.ResourcesNeeded);
                    bool sonIguales = coincidencias.OrderBy(x => x).SequenceEqual(suggestionRequest.ResourcesNeeded.OrderBy(x => x));

                    if (sonIguales)
                    {
                        oficinasConRecursos.Add(myOffi); //se le agrega filtro por recursos
                    }

                }
            
            var resultadoOffices = oficinasConRecursos.OrderByDescending(x => x.MaxCapacity);

            var resultadoOfficesVecindario = new List<OfficeSpecs>(); //nueva lista para retornar especificamente por preferencia de vecindario

            foreach (var item in resultadoOffices)
            {
                
                // si coincide el vecindario con el request y agrego a lista

                var localOfi = listLocations.Where(x => x.Name == item.LocationName).FirstOrDefault();
                var vecindarioOfi = localOfi.Neighborhood;

                if(vecindarioOfi == suggestionRequest.PreferedNeigborHood)
                {
                    resultadoOfficesVecindario.Add(item);
                }
            }

            var resultadoOfficesVecindarioYCap = resultadoOfficesVecindario.OrderByDescending(x => x.MaxCapacity);

                if (resultadoOfficesVecindarioYCap.Count() > 0)
                {
                    return resultadoOfficesVecindarioYCap;
                }
                else
                {
                    return resultadoOffices;
                }
            
        }
    }
}