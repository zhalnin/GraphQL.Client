﻿using Firebase.Auth;
using GraphQL.Client.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Client.Script
{
    public class LoginScript
    {
        private readonly FirebaseAuthProvider _firebaseAuthProvider;
        private readonly TokenStore _tokenStore;

        public LoginScript(FirebaseAuthProvider firebaseAuthProvider, TokenStore tokenStore) => 
            (_firebaseAuthProvider, _tokenStore) = (firebaseAuthProvider, tokenStore);


        public async Task Run()
        {
            Console.WriteLine("Enter your email:");
            string email = Console.ReadLine();

            Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();

            FirebaseAuthLink firebaseAuthLink = await _firebaseAuthProvider.SignInWithEmailAndPasswordAsync(email, password);
            _tokenStore.AccessToken = firebaseAuthLink.FirebaseToken;

            Console.WriteLine("Successfully logged in");
        }
    }
}
