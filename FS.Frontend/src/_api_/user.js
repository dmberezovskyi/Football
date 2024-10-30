// import faker from 'faker';
// import { sample } from 'lodash';
// import user from 'src/redux/slices/user';
// import { UserRole, UserState } from 'src/types';
// import mock from 'src/utils/mock';


// const createId = (index) => `fc68bad5-d430-4033-b8f8-4bc069dc0ba0-${index}`;
// const userId = '3474541f-cca3-4a58-a99f-d4cce9191204';
// const avatarUrl = "https://thispersondoesnotexist.com/image"
// faker.locale = "en"

// mock.onGet('/api/users/profile').reply((config) => {

//   const user = {
//     id: '3474541f-cca3-4a58-a99f-d4cce9191204',
//     firstName: faker.name.firstName(),
//     lastName: faker.name.lastName(),
//     email: faker.internet.email(),
//     photoURL: avatarUrl,
//     middleName: faker.name.lastName(),
//     birthDate: faker.date.between('1990-01-01', '2015-01-05'),
//     phone: faker.phone.phoneNumber(),
//     about: faker.lorem.lines(2),
//     isPublic: true,
//     role: sample([
//       UserRole.Admin,
//       UserRole.Player,
//       UserRole.Trainer
//     ]),
//     state: sample([
//       UserState.Inactive,
//       UserState.Active,
//       UserState.Locked,
//       UserState.Banned
//     ])
//   };

//   return [200, user];
// });

// mock.onGet(`/api/users/${userId}/info`).reply(() => {
//   const userInfo = {
//     id: '3474541f-cca3-4a58-a99f-d4cce9191204',
//     firstName: faker.name.firstName(),
//     lastName: faker.name.lastName(),
//     email: faker.internet.email(),
//     photoURL: avatarUrl,
//     role: sample([
//       UserRole.Admin,
//       UserRole.Player,
//       UserRole.Trainer
//     ]),
//     state: sample([
//       UserState.Inactive,
//       UserState.Active,
//       UserState.Locked,
//       UserState.Banned
//     ])
//   };

//   return [200, userInfo];
// });

// mock.onGet('/api/users').reply((config) => {
//   const take = config.params["take"];
//   const skip = config.params["skip"];

//   debugger;
//   if(!config.headers["Authorization"])
//     return [403];

//   const users = [...Array(30)].map((user, index) => {
//     const userIndex = index + 1;
    
//     const avatarUrl = "https://thispersondoesnotexist.com/image"
//     return {
//       id: createId(userIndex),
//       firstName: faker.name.firstName(),
//       lastName: faker.name.lastName(),
//       email: faker.internet.email(),
//       photoURL: avatarUrl,
//       middleName: faker.name.middleName(),
//       birthDate: faker.date.between('1990-01-01', '2015-01-05'),
//       phone: faker.phone.phoneNumber(),
//       about: faker.lorem.lines(2),
//       isPublic: true,
//       role: sample([
//         UserRole.Admin,
//         UserRole.Player,
//         UserRole.Trainer
//       ]),
//       state: sample([
//         UserState.Inactive,
//         UserState.Active,
//         UserState.Locked,
//         UserState.Banned
//       ])
//     };
//   });


//   return [200, {
//     users: users.slice(skip, take + skip),
//     total: user.length
//   }];
// });