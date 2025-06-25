' (c) Copyright Microsoft Corporation.
' This source is subject to the Microsoft Public License (Ms-PL).
' Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
' All other rights reserved.

' The example companies, organizations, products, domain names,
' e-mail addresses, logos, people, places, and events depicted
' herein are fictitious.  No association with any real company,
' organization, product, domain name, email address, logo, person,
' places, or events is intended or should be inferred.

Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations

Namespace OpenSilver.Samples.Showcase

    ''' <summary>
    ''' Represents a person's contact information.
    ''' </summary>
    Public Class Contact
        Implements INotifyPropertyChanged, IEditableObject

        ' Backing fields
        Private _firstName As String
        Private _lastName As String
        Private _phone As String
        Private _street1 As String
        Private _street2 As String
        Private _city As String
        Private _state As String
        Private _email As String
        Private _zip As Integer
        Private _isBusinessAddress As Boolean

        ' Keeps a copy of the original contact for editing.
        Private cache As Contact

        ' Constructors
        Public Sub New()
        End Sub

        ' Properties

        ''' <summary>Gets or sets the first name of the contact.</summary>
        <Required>
        <Display(Name:="First Name", GroupName:="Name")>
        Public Property FirstName As String
            Get
                Return _firstName
            End Get
            Set(value As String)
                If value <> _firstName Then
                    _firstName = value
                    OnPropertyChanged(NameOf(FirstName))
                End If
            End Set
        End Property

        ''' <summary>Gets or sets the last name of the contact.</summary>
        <Required>
        <Display(Name:="Last Name", GroupName:="Name")>
        Public Property LastName As String
            Get
                Return _lastName
            End Get
            Set(value As String)
                If value <> _lastName Then
                    _lastName = value
                    OnPropertyChanged(NameOf(LastName))
                End If
            End Set
        End Property

        ''' <summary>Gets or sets the phone number of the contact in the form (###) ###-####.</summary>
        <Display(Name:="Phone", Description:="Phone number of the form (###) ###-####")>
        <RegularExpression("^\(\d\d\d\) \d\d\d\-\d\d\d\d$", ErrorMessage:="Not a valid phone number.  Please enter a phone number that matches the format (###) ###-####")>
        Public Property Phone As String
            Get
                Return _phone
            End Get
            Set(value As String)
                If value <> _phone Then
                    Validator.ValidateProperty(value, New ValidationContext(Me, Nothing, Nothing) With {.MemberName = NameOf(Phone)})
                    _phone = value
                    OnPropertyChanged(NameOf(Phone))
                End If
            End Set
        End Property

        ''' <summary>Gets or sets the street address of the contact.</summary>
        <Required>
        <Display(Name:="Street Address", GroupName:="Address")>
        Public Property Street1 As String
            Get
                Return _street1
            End Get
            Set(value As String)
                If value <> _street1 Then
                    _street1 = value
                    OnPropertyChanged(NameOf(Street1))
                End If
            End Set
        End Property

        ''' <summary>Gets or sets the secondary street address information for the contact, such as apartment number or P.O. Box.</summary>
        <Display(Name:="Secondary Street Address", Description:="Additional street address information, such as apartment number or P.O. Box", GroupName:="Address")>
        Public Property Street2 As String
            Get
                Return _street2
            End Get
            Set(value As String)
                If value <> _street2 Then
                    _street2 = value
                    OnPropertyChanged(NameOf(Street2))
                End If
            End Set
        End Property

        ''' <summary>Gets or sets the city of the contact.</summary>
        <Required>
        <Display(Name:="City", Description:="City of residence", GroupName:="Address")>
        Public Property City As String
            Get
                Return _city
            End Get
            Set(value As String)
                If value <> _city Then
                    _city = value
                    OnPropertyChanged(NameOf(City))
                End If
            End Set
        End Property

        ''' <summary>Gets or sets the abbreviated state name of the contact (e.g. CA or TN).</summary>
        <Required>
        <RegularExpression("^[a-zA-Z][a-zA-Z]$", ErrorMessage:="Not a valid state abbreviation (e.g. CA or TN)")>
        <Display(Name:="State", Description:="State abbreviation", GroupName:="Address")>
        Public Property State As String
            Get
                Return _state
            End Get
            Set(value As String)
                If value <> _state Then
                    Validator.ValidateProperty(value, New ValidationContext(Me, Nothing, Nothing) With {.MemberName = NameOf(State)})
                    _state = value
                    OnPropertyChanged(NameOf(State))
                End If
            End Set
        End Property

        ''' <summary>Gets or sets a value indicating whether this contact's address is a business address.</summary>
        <Display(Name:="Business Address", Description:="Indicates that the address is a business address.")>
        Public Property IsBusinessAddress As Boolean
            Get
                Return _isBusinessAddress
            End Get
            Set(value As Boolean)
                If value <> _isBusinessAddress Then
                    _isBusinessAddress = value
                    OnPropertyChanged(NameOf(IsBusinessAddress))
                End If
            End Set
        End Property

        ''' <summary>Gets or sets the e-mail address (e.g. someone@somewhere.com) for the contact.</summary>
        <RegularExpression("^([a-zA-Z0-9\!\%\$\%\*\/\?\|\^\{\}\`\~\&\'\+\-\=_]\.?)*[a-zA-Z0-9\!\%\$\%\*\/\?\|\^\{\}\`\~\&\'\+\-\=_]@((([a-zA-Z0-9\!\%\$\%\*\/\?\|\^\{\}\`\~\&\'\+\-\=_]\.?)*[a-zA-Z0-9\!\%\$\%\*\/\?\|\^\{\}\`\~\&\'\+\-\=_])|(\[\d+\.\d+\.\d+\.\d+\]))$", ErrorMessage:="Not a valid e-mail address.")>
        <Display(Name:="Email", Description:="An e-mail address of the form <name>@<domain>, such as john@johndoe.com")>
        Public Property Email As String
            Get
                Return _email
            End Get
            Set(value As String)
                If value <> _email Then
                    Validator.ValidateProperty(value, New ValidationContext(Me, Nothing, Nothing) With {.MemberName = NameOf(Email)})
                    _email = value
                    OnPropertyChanged(NameOf(Email))
                End If
            End Set
        End Property

        ''' <summary>Gets or sets the zip code of the contact.</summary>
        <Required>
        <RegularExpression("^\d\d\d\d\d$", ErrorMessage:="Zip codes must be 5-digit numbers.")>
        <Display(Name:="Zip", Description:="Five-digit zip code", GroupName:="Address")>
        Public Property Zip As Integer
            Get
                Return _zip
            End Get
            Set(value As Integer)
                If value <> _zip Then
                    Validator.ValidateProperty(value, New ValidationContext(Me, Nothing, Nothing) With {.MemberName = NameOf(Zip)})
                    _zip = value
                    OnPropertyChanged(NameOf(Zip))
                End If
            End Set
        End Property

        ''' <summary>
        ''' Raises a property changed notification for the specified property name.
        ''' </summary>
        Protected Overridable Sub OnPropertyChanged(propName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propName))
        End Sub

        ' INotifyPropertyChanged implementation
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        ' Stock "John Doe" contact
        Public Shared ReadOnly Property JohnDoe As Contact
            Get
                Return New Contact With {
                    .FirstName = "John",
                    .LastName = "Doe",
                    .Phone = "(555) 555-5555",
                    .Street1 = "1 Anywhere Street",
                    .City = "Anytown",
                    .State = "WA",
                    .Zip = 98000
                }
            End Get
        End Property

        ' Stock group of contacts
        Public Shared ReadOnly Property People As IEnumerable(Of Contact)
            Get
                Return New ObservableCollection(Of Contact) From {
                    New Contact With {.FirstName = "Kim", .LastName = "Abercrombie", .Phone = "(555) 555-0000", .Street1 = "1 Anywhere Street", .City = "Anytown", .State = "WA", .Zip = 12345},
                    New Contact With {.FirstName = "Hadaya", .LastName = "Sagiv", .Phone = "(555) 555-0001", .Street1 = "2 Anywhere Street", .City = "AnotherTown", .State = "HI", .Zip = 55555},
                    New Contact With {.FirstName = "Jeff", .LastName = "Price", .Phone = "(555) 555-0002", .Street1 = "3 Somewhere Street", .City = "Anytown", .State = "OR", .Zip = 77777},
                    New Contact With {.FirstName = "Chris", .LastName = "Hill", .Phone = "(555) 555-0431", .Street1 = "17 Anywhere Lane", .City = "AnotherTown", .State = "WA", .Zip = 28145},
                    New Contact With {.FirstName = "Prithvi", .LastName = "Raj", .Phone = "(555) 555-8543", .Street1 = "9144 Somewhere Street", .City = "Anytown", .State = "OR", .Zip = 75812},
                    New Contact With {.FirstName = "Ryan", .LastName = "Ihrig", .Phone = "(555) 555-1345", .Street1 = "19 Anywhere Lane", .City = "AnotherTown", .State = "OR", .Zip = 58104},
                    New Contact With {.FirstName = "Jim", .LastName = "Ptaszynski", .Phone = "(555) 555-5832", .Street1 = "1 Anywhere Street", .City = "Anytown", .State = "NH", .Zip = 74584},
                    New Contact With {.FirstName = "Dan", .LastName = "Bacon", .Phone = "(555) 555-1914", .Street1 = "144 Anywhere Lane", .City = "Anytown", .State = "WA", .Zip = 13412},
                    New Contact With {.FirstName = "Steve", .LastName = "Kastner", .Phone = "(555) 555-4581", .Street1 = "121 Anywhere Street", .City = "AnotherTown", .State = "WA", .Zip = 45828},
                    New Contact With {.FirstName = "Andy", .LastName = "Ruth", .Phone = "(555) 555-4258", .Street1 = "391 Somewhere Lane", .City = "Anytown", .State = "WA", .Zip = 38447},
                    New Contact With {.FirstName = "Brian", .LastName = "Bredehoeft", .Phone = "(555) 555-4853", .Street1 = "1234 Anywhere Street", .City = "Anytown", .State = "WA", .Zip = 14834},
                    New Contact With {.FirstName = "Esko", .LastName = "Sario", .Phone = "(555) 555-3248", .Street1 = "1 Someplace Street", .City = "Anytown", .State = "CA", .Zip = 41834},
                    New Contact With {.FirstName = "Joel", .LastName = "Lachance", .Phone = "(555) 555-1482", .Street1 = "48 Anywhere Lane", .City = "Anytown", .State = "NY", .Zip = 91934},
                    New Contact With {.FirstName = "Bonnie", .LastName = "Skelly", .Phone = "(555) 555-8582", .Street1 = "1 Anywhere Street", .City = "Anytown", .State = "NY", .Zip = 95812},
                    New Contact With {.FirstName = "James", .LastName = "Doe", .Phone = "(555) 555-5555", .Street1 = "1 Anywhere Street", .City = "AnotherTown", .State = "WA", .Zip = 58433},
                    New Contact With {.FirstName = "John", .LastName = "Somebody", .Phone = "(555) 555-5555", .Street1 = "1 Anywhere Street", .City = "Anytown", .State = "WA", .Zip = 15852},
                    New Contact With {.FirstName = "Jane", .LastName = "Somebody", .Phone = "(555) 555-5555", .Street1 = "1 Anywhere Street", .City = "Anytown", .State = "HI", .Zip = 18824},
                    New Contact With {.FirstName = "Andrew", .LastName = "Ma", .Phone = "(555) 555-2384", .Street1 = "1 Anywhere Drive", .City = "Anytown", .State = "OR", .Zip = 12420},
                    New Contact With {.FirstName = "Shannon", .LastName = "Dascher", .Phone = "(555) 555-7378", .Street1 = "231 Anywhere Lane", .City = "Anytown", .State = "WA", .Zip = 14855},
                    New Contact With {.FirstName = "William", .LastName = "Looney", .Phone = "(555) 555-5555", .Street1 = "145 Somwhere Street", .City = "Anytown", .State = "OR", .Zip = 19855},
                    New Contact With {.FirstName = "Louise", .LastName = "Toubro", .Phone = "(555) 555-4832", .Street1 = "123 Somewhere Lane", .City = "AnotherTown", .State = "OR", .Zip = 12842},
                    New Contact With {.FirstName = "Tim", .LastName = "Toyoshima", .Phone = "(555) 555-3849", .Street1 = "1234 Anywhere Street", .City = "Anytown", .State = "NH", .Zip = 12843},
                    New Contact With {.FirstName = "Peter", .LastName = "Mullen", .Phone = "(555) 555-7758", .Street1 = "1233 Anywhere Street", .City = "Anytown", .State = "WA", .Zip = 12484},
                    New Contact With {.FirstName = "Ming-Yang", .LastName = "Xie", .Phone = "(555) 555-9283", .Street1 = "1423 Somewhere Street", .City = "Anytown", .State = "WA", .Zip = 12348},
                    New Contact With {.FirstName = "Smuel", .LastName = "Yair", .Phone = "(555) 555-4821", .Street1 = "138 Anywhere Street", .City = "Anytown", .State = "WA", .Zip = 48439},
                    New Contact With {.FirstName = "John", .LastName = "Woods", .Phone = "(555) 555-5859", .Street1 = "9583 Anywhere Street", .City = "Anytown", .State = "WA", .Zip = 93205},
                    New Contact With {.FirstName = "Pieter", .LastName = "Wycoff", .Phone = "(555) 555-9292", .Street1 = "321 Anywhere Lane", .City = "Anytown", .State = "CA", .Zip = 95839},
                    New Contact With {.FirstName = "Michiel", .LastName = "Wories", .Phone = "(555) 555-8584", .Street1 = "199 Anywhere Street", .City = "AnotherTown", .State = "NY", .Zip = 39495},
                    New Contact With {.FirstName = "John", .LastName = "Yokim", .Phone = "(555) 555-4843", .Street1 = "3241 Someplace Street", .City = "Anytown", .State = "NY", .Zip = 93258},
                    New Contact With {.FirstName = "Yanlai", .LastName = "Guo", .Phone = "(555) 555-4999", .Street1 = "2241 Somewhere Street", .City = "Anytown", .State = "WA", .Zip = 18444}
                }
            End Get
        End Property

        ' IEditableObject Members

        Public Sub BeginEdit() Implements IEditableObject.BeginEdit
            cache = New Contact With {
                .City = Me.City,
                .Email = Me.Email,
                .FirstName = Me.FirstName,
                .LastName = Me.LastName,
                .Phone = Me.Phone,
                .State = Me.State,
                .Street1 = Me.Street1,
                .Street2 = Me.Street2,
                .Zip = Me.Zip
            }
        End Sub

        Public Sub CancelEdit() Implements IEditableObject.CancelEdit
            If cache IsNot Nothing Then
                Me.City = cache.City
                Me.Email = cache.Email
                Me.FirstName = cache.FirstName
                Me.LastName = cache.LastName
                Me.Phone = cache.Phone
                Me.State = cache.State
                Me.Street1 = cache.Street1
                Me.Street2 = cache.Street2
                Me.Zip = cache.Zip
                cache = Nothing
            End If
        End Sub

        Public Sub EndEdit() Implements IEditableObject.EndEdit
            cache = Nothing
        End Sub

    End Class

End Namespace
