# Samples.Universal
Sample of LogoFX usage for Universal Apps.

This sample demonstrates simple scenario of providing user credentials and displaying data 
while implementing all Model and subsequent layers in `.NETStandard 2.0`.

This sample uses **Fake Providers** approach and also implements End-To-End testing with **FlaUI**.

Right now it might fail to compile in the **fb-netstandard** branch. If it does make sure you only reference 
one of the two following libraries: **Fake.Providers** or **Infra.Providers**.

The **Infra.Providers** also has an issue with UWP and accessing `SerializedBuildersCollection.Data` file - currently under investigation
