# Samples.Universal
Sample of LogoFX usage for Universal Apps.

This sample demonstrates simple scenario of providing user credentials and displaying data 
while implementing all Model and subsequent layers in `.NETStandard 2.0`.

This sample uses **Fake Providers** approach and also implements End-To-End testing with **FlaUI**.

Right now it might fail to compile. If it does make sure you only reference 
one of the two following libraries: **Fake.Providers** or **Infra.Providers**. 
Another cause may be the missing assemblies. Try to copy manually one of the following:
**Fake.ProviderBuilders**, **Attest.Fake.Builders**, **Attest.Fake.Core**, **Attest.Fake.Setup**, **Patterns.Visitor**

There is one missing step which is copying the Data file to the output folder - to be implemented at the solution level
