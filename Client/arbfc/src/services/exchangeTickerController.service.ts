import axios from "axios";

const API_URL = "exchangecommon/get_tickers";

const getTickersData = async (chooseAlert: any) => {
    return await axios
        .post(API_URL, chooseAlert)
        .then((response) => {
            return response.data
        })
};

const ExchangeTicketsService = {
    getTickersData
};

export default ExchangeTicketsService;