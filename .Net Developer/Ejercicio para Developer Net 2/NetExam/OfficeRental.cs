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
                listLocations.Add(locationSpecs);
            
        }

        public void AddOffice(OfficeSpecs officeSpecs)
        {
            listOffices.Add(officeSpecs);
            
        }

        public void BookOffice(BookingRequest bookingRequest)
        {
            //deberia pregu8ntar si esa oficina no esta reservada ese día
            var reservas = GetBookings(bookingRequest.LocationName, bookingRequest.OfficeName);
            
            //foreach(var item in reservas)
            //{
            //    if(item.DateTime == bookingRequest.DateTime)
            //    {

            //        throw new InvalidOperationException("hola?");
            //    }
            //    else
            //    {
            //        listBookingRequests.Add(bookingRequest);
            //    }
            //}

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
            List<OfficeSpecs> listOfficeSuggestion = new List<OfficeSpecs>();

            foreach (var item in listOffices)
            {
                if(suggestionRequest.CapacityNeeded > item.MaxCapacity)//cumple necesidad de capacidad
                {
                   // //preguntar por vecindario al final
                   // var local = item.LocationName;
                   // var vecindario = listLocations.Where(x => x.Name == local).;
                   //if(suggestionRequest.PreferedNeigborHood == item..)
                   // {
                   //     //agregar oficina a la lista
                   // }
                    
                    listOfficeSuggestion.Add(item);
                }
            }

            return listOfficeSuggestion;
        }
    }
}