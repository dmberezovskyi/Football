export enum UserRole {
    Admin = "admin",
    Player = "player",
    Trainer = "trainer"
}

export enum UserStatus {
    Inactive = 0,
    Active = 1,
    Locked = 2,
    Banned = 3
}

export interface User {
    id: string;
    version: number;

    profile: UserProfile
    snippet: UserSnippet
    userInfo: UserInfo
    userInfoDetailed: UserInfoDetailed
    //TODO Add on backend

    photoURL: string;
    about: string;
    state: UserStatus;
}

export interface UserInfoDetailed {
    status: UserStatus,
    createdOn: Date,
    updatedOn: Date
    //emailConfirmed: boolean
}
export interface UserSnippet {
    firstName: string;
    lastName: string;
    middleName: string;
    birthDate: Date;
    role: UserRole;
}

export interface UserProfile {
    firstName: string;
    lastName: string;
    middleName: string;
    birthDate: Date;
    phone: string;
    address: UserAddress;
}

interface UserAddress {
    country: string;
    state: string;
    city: string;
    zipCode: string;
    streetAddress: string;
}

export interface UserInfo {
    firstName: string;
    lastName: string;
    role: string;
    email: string;
}

export interface QueryResponse<T> {
    items: T[];
    total: number;
}

export enum QueryPart {
    Snippet = "snippet",
    Profile = "profile",
    Team = "team",
    Organization = "organization",
    UserInfo = "userInfo",
    UserInfoDetailed = "userInfoDetailed"
}
export interface Error {
    errorCode: string;
    location: string;
    message: string;
    reason: string;
    values: ErrorValues
}

export interface ErrorValues  {
    [value: string]: any
}

export enum SidebarConfigType {
    General,
    Admin
}