## The idea

Spinit.Security has some useful classes and interfaces for handling passwords in a secure way

## Getting started

To install Spinit.Security, run the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console)

<div class="nuget-badge">
        <p><code>PM&gt; Install-Package Spinit.Security</code></p>
</div>

## Password Service

A class for securely hashing passwords. The class implementes `IPasswordService` and the implementation depends on an `IPasswordHashService` and a `IPasswordSaltService`.

    var hashService = new PasswordHashService();
    var saltService = new PasswordSaltService();
    var passwordService = new PasswordService(hashService, saltService);

    // create a hashed password with salt like this
    var hash = passwordService.Create("correct horse battery staple");

    // verify password against an existing hash and salt
    var isValidPassword = passwordService.VerifyPassword(hash.HashedPassword, "correct horse battery staple", hash.Salt);
