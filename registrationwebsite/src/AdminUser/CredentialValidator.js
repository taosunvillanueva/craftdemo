import React from 'react'

export function Validate(name, passord) {
  const errors = [];

  if (name === undefined || name === null || name.length === 0) {
    errors.push("Name can't be empty");
  }

  if (passord === undefined || passord === null || passord.length === 0) {
    errors.push("Password can't be empty");
  }

  return errors;
}