namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.Collections.ObjectModel
open System.ComponentModel
open System.ComponentModel.DataAnnotations

[<AllowNullLiteral>]
type Contact() as this =
    // Backing fields
    let mutable firstName = null
    let mutable lastName = null
    let mutable phone = null
    let mutable street1 = null
    let mutable street2 = null
    let mutable city = null
    let mutable state = null
    let mutable email = null
    let mutable zip = 0
    let mutable isBusinessAddress = false
    let mutable cache : Contact = null

    let propertyChanged = Event<_,_>()

    static let johnDoe =
        Contact(
            FirstName = "John",
            LastName = "Doe",
            Phone = "(555) 555-5555",
            Street1 = "1 Anywhere Street",
            City = "Anytown",
            State = "WA",
            Zip = 98000
        )
    static let people =
        ObservableCollection<Contact>([
            Contact(FirstName = "Kim",      LastName = "Abercrombie", Phone = "(555) 555-0000", Street1 = "1 Anywhere Street", City = "Anytown",    State = "WA", Zip = 12345)
            Contact(FirstName = "Hadaya",   LastName = "Sagiv",       Phone = "(555) 555-0001", Street1 = "2 Anywhere Street", City = "AnotherTown",State = "HI", Zip = 55555)
            Contact(FirstName = "Jeff",     LastName = "Price",       Phone = "(555) 555-0002", Street1 = "3 Somewhere Street", City = "Anytown",   State = "OR", Zip = 77777)
            Contact(FirstName = "Chris",    LastName = "Hill",        Phone = "(555) 555-0431", Street1 = "17 Anywhere Lane", City = "AnotherTown", State = "WA", Zip = 28145)
            Contact(FirstName = "Prithvi",  LastName = "Raj",         Phone = "(555) 555-8543", Street1 = "9144 Somewhere Street", City = "Anytown", State = "OR", Zip = 75812)
            Contact(FirstName = "Ryan",     LastName = "Ihrig",       Phone = "(555) 555-1345", Street1 = "19 Anywhere Lane", City = "AnotherTown", State = "OR", Zip = 58104)
            Contact(FirstName = "Jim",      LastName = "Ptaszynski",  Phone = "(555) 555-5832", Street1 = "1 Anywhere Street", City = "Anytown",    State = "NH", Zip = 74584)
            Contact(FirstName = "Dan",      LastName = "Bacon",       Phone = "(555) 555-1914", Street1 = "144 Anywhere Lane", City = "Anytown",    State = "WA", Zip = 13412)
            Contact(FirstName = "Steve",    LastName = "Kastner",     Phone = "(555) 555-4581", Street1 = "121 Anywhere Street", City = "AnotherTown", State = "WA", Zip = 45828)
            Contact(FirstName = "Andy",     LastName = "Ruth",        Phone = "(555) 555-4258", Street1 = "391 Somewhere Lane", City = "Anytown",   State = "WA", Zip = 38447)
            Contact(FirstName = "Brian",    LastName = "Bredehoeft",  Phone = "(555) 555-4853", Street1 = "1234 Anywhere Street", City = "Anytown", State = "WA", Zip = 14834)
            Contact(FirstName = "Esko",     LastName = "Sario",       Phone = "(555) 555-3248", Street1 = "1 Someplace Street", City = "Anytown",   State = "CA", Zip = 41834)
            Contact(FirstName = "Joel",     LastName = "Lachance",    Phone = "(555) 555-1482", Street1 = "48 Anywhere Lane", City = "Anytown",     State = "NY", Zip = 91934)
            Contact(FirstName = "Bonnie",   LastName = "Skelly",      Phone = "(555) 555-8582", Street1 = "1 Anywhere Street", City = "Anytown",    State = "NY", Zip = 95812)
            Contact(FirstName = "James",    LastName = "Doe",         Phone = "(555) 555-5555", Street1 = "1 Anywhere Street", City = "AnotherTown", State = "WA", Zip = 58433)
            Contact(FirstName = "John",     LastName = "Somebody",    Phone = "(555) 555-5555", Street1 = "1 Anywhere Street", City = "Anytown",    State = "WA", Zip = 15852)
            Contact(FirstName = "Jane",     LastName = "Somebody",    Phone = "(555) 555-5555", Street1 = "1 Anywhere Street", City = "Anytown",    State = "HI", Zip = 18824)
            Contact(FirstName = "Andrew",   LastName = "Ma",          Phone = "(555) 555-2384", Street1 = "1 Anywhere Drive", City = "Anytown",     State = "OR", Zip = 12420)
            Contact(FirstName = "Shannon",  LastName = "Dascher",     Phone = "(555) 555-7378", Street1 = "231 Anywhere Lane", City = "Anytown",    State = "WA", Zip = 14855)
            Contact(FirstName = "William",  LastName = "Looney",      Phone = "(555) 555-5555", Street1 = "145 Somwhere Street", City = "Anytown",  State = "OR", Zip = 19855)
            Contact(FirstName = "Louise",   LastName = "Toubro",      Phone = "(555) 555-4832", Street1 = "123 Somewhere Lane", City = "AnotherTown", State = "OR", Zip = 12842)
            Contact(FirstName = "Tim",      LastName = "Toyoshima",   Phone = "(555) 555-3849", Street1 = "1234 Anywhere Street", City = "Anytown", State = "NH", Zip = 12843)
            Contact(FirstName = "Peter",    LastName = "Mullen",      Phone = "(555) 555-7758", Street1 = "1233 Anywhere Street", City = "Anytown", State = "WA", Zip = 12484)
            Contact(FirstName = "Ming-Yang",LastName = "Xie",         Phone = "(555) 555-9283", Street1 = "1423 Somewhere Street", City = "Anytown", State = "WA", Zip = 12348)
            Contact(FirstName = "Smuel",    LastName = "Yair",        Phone = "(555) 555-4821", Street1 = "138 Anywhere Street", City = "Anytown",   State = "WA", Zip = 48439)
            Contact(FirstName = "John",     LastName = "Woods",       Phone = "(555) 555-5859", Street1 = "9583 Anywhere Street", City = "Anytown", State = "WA", Zip = 93205)
            Contact(FirstName = "Pieter",   LastName = "Wycoff",      Phone = "(555) 555-9292", Street1 = "321 Anywhere Lane", City = "Anytown",    State = "CA", Zip = 95839)
            Contact(FirstName = "Michiel",  LastName = "Wories",      Phone = "(555) 555-8584", Street1 = "199 Anywhere Street", City = "AnotherTown", State = "NY", Zip = 39495)
            Contact(FirstName = "John",     LastName = "Yokim",       Phone = "(555) 555-4843", Street1 = "3241 Someplace Street", City = "Anytown", State = "NY", Zip = 93258)
            Contact(FirstName = "Yanlai",   LastName = "Guo",         Phone = "(555) 555-4999", Street1 = "2241 Somewhere Street", City = "Anytown", State = "WA", Zip = 18444)
        ])

    member private this.OnPropertyChanged propName =
        propertyChanged.Trigger(this, PropertyChangedEventArgs(propName))

    [<Required>]
    [<Display(Name = "First Name", GroupName = "Name")>]
    member this.FirstName
        with get() = firstName
        and set value =
            if value <> firstName then
                firstName <- value
                this.OnPropertyChanged("FirstName")

    [<Required>]
    [<Display(Name = "Last Name", GroupName = "Name")>]
    member this.LastName
        with get() = lastName
        and set value =
            if value <> lastName then
                lastName <- value
                this.OnPropertyChanged("LastName")

    [<Display(Name = "Phone", Description = "Phone number of the form (###) ###-####")>]
    [<RegularExpression(@"^\(\d\d\d\) \d\d\d\-\d\d\d\d$", ErrorMessage = "Not a valid phone number.  Please enter a phone number that matches the format (###) ###-####")>]
    member this.Phone
        with get() = phone
        and set value =
            if value <> phone then
                Validator.ValidateProperty(value, ValidationContext(this, null, null, MemberName = "Phone"))
                phone <- value
                this.OnPropertyChanged("Phone")

    [<Required>]
    [<Display(Name = "Street Address", GroupName = "Address")>]
    member this.Street1
        with get() = street1
        and set value =
            if value <> street1 then
                street1 <- value
                this.OnPropertyChanged("Street1")

    [<Display(Name = "Secondary Street Address", Description = "Additional street address information, such as apartment number or P.O. Box", GroupName = "Address")>]
    member this.Street2
        with get() = street2
        and set value =
            if value <> street2 then
                street2 <- value
                this.OnPropertyChanged("Street2")

    [<Required>]
    [<Display(Name = "City", Description = "City of residence", GroupName = "Address")>]
    member this.City
        with get() = city
        and set value =
            if value <> city then
                city <- value
                this.OnPropertyChanged("City")

    [<Required>]
    [<RegularExpression(@"^[a-zA-Z][a-zA-Z]$", ErrorMessage = "Not a valid state abbreviation (e.g. CA or TN)")>]
    [<Display(Name = "State", Description = "State abbreviation", GroupName = "Address")>]
    member this.State
        with get() = state
        and set value =
            if value <> state then
                Validator.ValidateProperty(value, ValidationContext(this, null, null, MemberName = "State"))
                state <- value
                this.OnPropertyChanged("State")

    [<Display(Name = "Business Address", Description = "Indicates that the address is a business address.")>]
    member this.IsBusinessAddress
        with get() = isBusinessAddress
        and set value =
            if value <> isBusinessAddress then
                isBusinessAddress <- value
                this.OnPropertyChanged("IsBusinessAddress")

    [<RegularExpression(@"^([a-zA-Z0-9\!\%\$\%\*\/\?\|\^\{\}\`\~\&\'\+\-\=_]\.?)*[a-zA-Z0-9\!\%\$\%\*\/\?\|\^\{\}\`\~\&\'\+\-\=_]@((([a-zA-Z0-9\!\%\$\%\*\/\?\|\^\{\}\`\~\&\'\+\-\=_]\.?)*[a-zA-Z0-9\!\%\$\%\*\/\?\|\^\{\}\`\~\&\'\+\-\=_])|(\[\d+\.\d+\.\d+\.\d+\]))$", ErrorMessage = "Not a valid e-mail address.")>]
    [<Display(Name = "Email", Description = "An e-mail address of the form <name>@<domain>, such as john@johndoe.com")>]
    member this.Email
        with get() = email
        and set value =
            if value <> email then
                Validator.ValidateProperty(value, ValidationContext(this, null, null, MemberName = "Email"))
                email <- value
                this.OnPropertyChanged("Email")

    [<Required>]
    [<RegularExpression(@"^\d\d\d\d\d$", ErrorMessage = "Zip codes must be 5-digit numbers.")>]
    [<Display(Name = "Zip", Description = "Five-digit zip code", GroupName = "Address")>]
    member this.Zip
        with get() = zip
        and set value =
            if value <> zip then
                Validator.ValidateProperty(value, ValidationContext(this, null, null, MemberName = "Zip"))
                zip <- value
                this.OnPropertyChanged("Zip")

    // INotifyPropertyChanged
    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member _.PropertyChanged = propertyChanged.Publish

    // IEditableObject
    interface IEditableObject with
        member _.BeginEdit() =
            cache <- Contact()
            cache.FirstName <- this.FirstName
            cache.LastName <- this.LastName
            cache.Phone <- this.Phone
            cache.Street1 <- this.Street1
            cache.Street2 <- this.Street2
            cache.City <- this.City
            cache.State <- this.State
            cache.Email <- this.Email
            cache.Zip <- this.Zip
            cache.IsBusinessAddress <- this.IsBusinessAddress

        member _.CancelEdit() =
            if not (isNull cache) then
                this.FirstName <- cache.FirstName
                this.LastName <- cache.LastName
                this.Phone <- cache.Phone
                this.Street1 <- cache.Street1
                this.Street2 <- cache.Street2
                this.City <- cache.City
                this.State <- cache.State
                this.Email <- cache.Email
                this.Zip <- cache.Zip
                this.IsBusinessAddress <- cache.IsBusinessAddress
                cache <- null

        member _.EndEdit() =
            cache <- null

    static member JohnDoe = johnDoe
    static member People : IEnumerable<Contact> = upcast people
