import { capitalize } from 'lodash';

// ----------------------------------------------------------------------

function getFirstCharacter(firstName, lastName) {
  return `${capitalize(firstName && firstName.charAt(0))}${capitalize(lastName && lastName.charAt(0))}`
}

export default function createAvatar(firstName, lastName) {
  return {
    name: getFirstCharacter(firstName, lastName),
    color: 'primary'
  };
}
