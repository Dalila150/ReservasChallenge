namespace NetExam
{
    using System;
    using System.Collections.Generic;
    using NetExam.Abstractions;
    using NetExam.Dto;

    public class OfficeRental : IOfficeRental
    {
        public void AddLocation(LocationSpecs locationSpecs)
        {
            throw new NotImplementedException();
        }

        public void AddOffice(OfficeSpecs officeSpecs)
        {
            throw new NotImplementedException();
        }

        public void BookOffice(BookingRequest bookingRequest)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IBooking> GetBookings(string locationName, string officeName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ILocation> GetLocations()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IOffice> GetOffices(string locationName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IOffice> GetOfficeSuggestion(SuggestionRequest suggestionRequest)
        {
            throw new NotImplementedException();
        }
    }
}