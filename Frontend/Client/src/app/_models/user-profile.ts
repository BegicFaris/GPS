export interface UserProfile {
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    birthDate?: string;
    registrationDate?: string;
    image?: string;
    address?: string;
    status?: boolean;
    
    // Specific fields based on user type
    license?: string;          // For Driver
    driversLicenseNumber?: string;  // For Driver
    hireDate?: string;        // For Driver/Manager
    workingHoursInAWeek?: number;   // For Driver
    department?: string;      // For Manager
    managerLevel?: string;    // For Manager
    discountID?: number;      // For Passenger
  }