import axios from "../../utils/axios";

export class GlobalizationApiClient {
    LoadTranslations(): void {
      axios.get("/translations")
    }
  }