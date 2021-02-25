namespace Learning_MVVM_Pattern.Models
{
    internal class Customer
    {
        private delegate void SetEventManager(bool setValue);
        private readonly SetEventManager _validityUpdateEvent;
        public bool IsValid { get; private set; }

        public Customer()
        {
            _validityUpdateEvent += SetValid;
            Validator = new CValidator(_validityUpdateEvent);
        }

        private void SetValid(bool setValue) {
            IsValid = setValue;
        }

        private static CValidator Validator { get; set; }

        public CDetails Details { get; private set; } = new CDetails();

        public class CDetails
        {
            private string _name;
            public CAddress Address { get; private set; } = new CAddress();

            public void SetName(string name)
            {
                _name = name;
                Validator.NameSetEvent.Invoke(name.Length > 0);
            }
            public string GetName()
            {
                return _name;
            }
        }

        public class CAddress
        {
            private string _firstAddressLine;
            private string _city;
            private string _postcode;

            public void SetFullAddress(string addressLine, string city, string postcode)
            {
                _firstAddressLine = addressLine;
                _city = city;
                _postcode = postcode;

                Validator.AddressSetEvent.Invoke(addressLine.Length > 0);
                Validator.CitySetEvent.Invoke(city.Length > 0);
                Validator.PostcodeSetEvent.Invoke(postcode.Length > 0);
            }
            
            public (string, string, string) GetFullAddress()
            {
                return (_firstAddressLine, _city, _postcode);
            }

            public string GetAddressString()
            {
                return GetAddressLine() + ", " + GetCity() + ", " + GetPostcode();
            }
            
            public void SetAddressLine(string firstAddress) 
            {
                _firstAddressLine = firstAddress;
                Validator.AddressSetEvent.Invoke(firstAddress.Length > 0);
            }
            
            public string GetAddressLine()
            {
                return _firstAddressLine;
            }

            public void SetCity(string city)
            {
                _city = city;
                Validator.CitySetEvent.Invoke(city.Length > 0);
            }
            
            public string GetCity()
            {
                return _city;
            }
            
            public void SetPostcode(string postcode)
            {
                _postcode = postcode;
                Validator.PostcodeSetEvent.Invoke(postcode.Length > 0);
            }
            
            public string GetPostcode()
            {
                return _postcode;
            }
        }

        private class CValidator
        {
            
            public readonly SetEventManager AddressSetEvent;
            public readonly SetEventManager CitySetEvent;
            public readonly SetEventManager PostcodeSetEvent;
            public readonly SetEventManager NameSetEvent;
            private readonly SetEventManager _validityUpdateEvent;

            private bool _addressLineSet;
            private bool _citySet;
            private bool _postcodeSet;
            private bool _nameSet;

            internal CValidator(SetEventManager validityUpdateEvent)
            {
                AddressSetEvent += SetAddressLine;
                CitySetEvent += SetCity;
                PostcodeSetEvent += SetPostcode;
                NameSetEvent += SetName;

                _validityUpdateEvent = validityUpdateEvent;
            }

            private bool ValidityCheck()
            {
                return GetName() && GetCity() && GetPostcode() && GetAddressLine();
            }

            private void SetAddressLine(bool setValue) {
                _addressLineSet = setValue;
                _validityUpdateEvent.Invoke(ValidityCheck());
            }
            
            private void SetCity(bool setValue) {
                _citySet = setValue;
                _validityUpdateEvent.Invoke(ValidityCheck());
            }
            
            private void SetPostcode(bool setValue) {
                _postcodeSet = setValue;
                _validityUpdateEvent.Invoke(ValidityCheck());
            }
            
            private void SetName(bool setValue) {
                _nameSet = setValue;
                _validityUpdateEvent.Invoke(ValidityCheck());
            }
            
            private bool GetAddressLine()
            {
                return _addressLineSet;
            }
            
            private bool GetCity()
            {
                return _citySet;
            }
            
            private bool GetPostcode()
            {
                return _postcodeSet;
            }
            
            private bool GetName()
            {
                return _nameSet;
            }
        }
        
        public override string ToString()
        {
            if (IsValid) {
                return Details.GetName() + " - " + Details.Address.GetAddressString();
            }

            return "Customer is not Setup!";
        }
    }
}