import { Photo } from "./photo"

export interface Member {
    id: number
    username: string
    age: number
    photoUrl: string
    created: Date
    lastActive: Date
    gender: string
    knownAs: string
    lookingfor: string
    interests: string
    introduction: string
    city: string
    country: string
    photos: Photo[]
  }
  
  