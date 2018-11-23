using System;
using System.Security.Cryptography;

namespace Spinit.Security.Password
{
    public class PasswordCreator : IPasswordCreator
    {
        private RandomNumberGenerator _rng;

        public PasswordCreator()
        {
            _rng = RandomNumberGenerator.Create();
        }

        public string CreatePassword(IPasswordParameters parameters)
        {
            return CreatePassword(parameters, null);
        }

        public string CreatePassword(IPasswordParameters parameters, IPasswordChecker user)
        {
            if ((parameters.MinPasswordLength <= 0) || (parameters.MaxPasswordLength <= 0)
                || (parameters.MinPasswordLength > parameters.MaxPasswordLength))
            {
                return null;
            }

            string newPassword;
            var checkExisting = false;
            do
            {
                var noOfGroups = 0;
                if (parameters.UseUpperCaseCharacters && (parameters.PasswordCharsUpperCase.Length > 0))
                {
                    noOfGroups++;
                }

                if (parameters.UseLowerCaseCharacters && (parameters.PasswordCharsLowerCase.Length > 0))
                {
                    noOfGroups++;
                }

                if (parameters.UseNumericCharacters && (parameters.PasswordCharsNumeric.Length > 0))
                {
                    noOfGroups++;
                }

                if (parameters.UseSpecialCharacters && (parameters.PasswordCharsSpecial.Length > 0))
                {
                    noOfGroups++;
                }

                var chrGrps = new char[noOfGroups][];

                noOfGroups = 0;
                if (parameters.UseUpperCaseCharacters && (parameters.PasswordCharsUpperCase.Length > 0))
                {
                    chrGrps[noOfGroups++] = parameters.PasswordCharsUpperCase.ToCharArray();
                }

                if (parameters.UseLowerCaseCharacters && (parameters.PasswordCharsLowerCase.Length > 0))
                {
                    chrGrps[noOfGroups++] = parameters.PasswordCharsLowerCase.ToCharArray();
                }

                if (parameters.UseNumericCharacters && (parameters.PasswordCharsNumeric.Length > 0))
                {
                    chrGrps[noOfGroups++] = parameters.PasswordCharsNumeric.ToCharArray();
                }

                if (parameters.UseSpecialCharacters && (parameters.PasswordCharsSpecial.Length > 0))
                {
                    chrGrps[noOfGroups] = parameters.PasswordCharsSpecial.ToCharArray();
                }

                var chrsLeftInGrp = new int[chrGrps.Length];

                for (var i = 0; i < chrsLeftInGrp.Length; i++)
                {
                    chrsLeftInGrp[i] = chrGrps[i].Length;
                }

                var leftGrpsOrder = new int[chrGrps.Length];

                for (var i = 0; i < leftGrpsOrder.Length; i++)
                {
                    leftGrpsOrder[i] = i;
                }

                var rndBts = new byte[4];
                
                _rng.GetBytes(rndBts);

                // Convert to 32-bit int.
                var seed = ((rndBts[0] & 0x7f) << 24) | (rndBts[1] << 16) | (rndBts[2] << 8) | rndBts[3];

                var rnd = new Random(seed);

                var pwd = parameters.MinPasswordLength < parameters.MaxPasswordLength
                              ? new char[rnd.Next(parameters.MinPasswordLength, parameters.MaxPasswordLength + 1)]
                              : new char[parameters.MinPasswordLength];

                var lastLeftGrpsOrderInx = leftGrpsOrder.Length - 1;

                for (var i = 0; i < pwd.Length; i++)
                {
                    var nextLeftGrpsOrderInx = lastLeftGrpsOrderInx == 0 ? 0 : rnd.Next(0, lastLeftGrpsOrderInx);

                    var nextGrpInx = leftGrpsOrder[nextLeftGrpsOrderInx];
                    var lastChrIdx = chrsLeftInGrp[nextGrpInx] - 1;

                    var nextChrInx = lastChrIdx == 0 ? 0 : rnd.Next(0, lastChrIdx + 1);

                    pwd[i] = chrGrps[nextGrpInx][nextChrInx];

                    if (lastChrIdx == 0)
                    {
                        chrsLeftInGrp[nextGrpInx] = chrGrps[nextGrpInx].Length;
                    }
                    else
                    {
                        if (lastChrIdx != nextChrInx)
                        {
                            var tmp = chrGrps[nextGrpInx][lastChrIdx];
                            chrGrps[nextGrpInx][lastChrIdx] = chrGrps[nextGrpInx][nextChrInx];
                            chrGrps[nextGrpInx][nextChrInx] = tmp;
                        }

                        chrsLeftInGrp[nextGrpInx]--;
                    }

                    if (lastLeftGrpsOrderInx == 0)
                    {
                        lastLeftGrpsOrderInx = leftGrpsOrder.Length - 1;
                    }
                    else
                    {
                        if (lastLeftGrpsOrderInx != nextLeftGrpsOrderInx)
                        {
                            var tmp = leftGrpsOrder[lastLeftGrpsOrderInx];
                            leftGrpsOrder[lastLeftGrpsOrderInx] = leftGrpsOrder[nextLeftGrpsOrderInx];
                            leftGrpsOrder[nextLeftGrpsOrderInx] = tmp;
                        }

                        lastLeftGrpsOrderInx--;
                    }
                }

                newPassword = new string(pwd);

                if (user != null)
                {
                    if (user.CheckIfPasswordExist(newPassword))
                    {
                        checkExisting = true;
                    }
                }
            }
            while (checkExisting);

            return newPassword;
        }
    }
}