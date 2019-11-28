import React from 'react'

export function Validate(name, email, location, interest, size) {
  const errors = [];

  if (name === undefined || name === null || name.length === 0) {
    errors.push("Name can't be empty");
  }

  if (email === undefined || email === null || email.length < 5) {
    errors.push("Email should be at least 5 charcters long");
  }

  if (email !== undefined && email !== null && email.split("").filter(x => x === "@").length !== 1) {
    errors.push("Email should contain a @");
  }

  if (email !== undefined && email !== null && email.indexOf(".") === -1) {
    errors.push("Email should contain at least one dot");
  }

  if (location === undefined || location === '' || location === null) {
      errors.push("Office location must be selected");
  }

  if (interest === undefined || interest === '' || interest === null) {
      errors.push("Please specify your security interest");
  }

  if (size === undefined || size === '' || size ===  null) {
      errors.push("Please pick a t-shirt size");
  }

  return errors;
}