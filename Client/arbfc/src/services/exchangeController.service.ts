import axios from "axios";

const API_URL = "exchange";

const getExchangeData = async () => {
    return await axios
        .get(API_URL)
        .then((response) => {
            return response.data
        })
};

const ExchangeService = {
    getExchangeData
};

export default ExchangeService;