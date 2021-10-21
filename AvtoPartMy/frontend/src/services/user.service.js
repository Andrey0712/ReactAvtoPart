import http from "../http_common";

class UsersService {
    get_list() {
        return http.get("api/Users");
    }
}

export default new UsersService();