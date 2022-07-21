import { Button, Card, Checkbox, Col, Form, Input, InputNumber, List, Modal, Radio, Row, Select, Space, Spin, Typography } from "antd";
import { useForm } from "antd/lib/form/Form";
import { Content } from "antd/lib/layout/layout";
import moment from "moment";
import React from "react";
import {
    CloseCircleOutlined
} from '@ant-design/icons';
import { FunctionComponent, MutableRefObject, useCallback, useEffect, useRef, useState } from "react"
import { useTranslation } from "react-i18next";
import { IExchangeChain, IExchangeChainTickers } from "../../interfaces";
import ExchangeService from "../../services/exchangeController.service";
import ExchangeTicketsService from "../../services/exchangeTickerController.service";

const { Option } = Select;

const Notification: FunctionComponent = () => {
    const { t } = useTranslation("common");
    // Modal
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [isLinkModalVisible, setIsLinkModalVisible] = useState(false);
    // Load first data
    const [exchangeChainArray, setExchangeChainArray] = useState<IExchangeChain[]>();
    const [exchangeTickersArray, setExchangeTickersArray] = useState<IExchangeChainTickers[]>();
    // Spinner
    const [tickersIsLoading, setTickersIsLoading] = useState(false);
    // transferState for exchangeLinks
    const exchangeArray = useRef<any>([]);
    // chainLinksData
    const [chainLinks, setChainLinks] = useState<IExchangeChain[]>();
    console.log(chainLinks);

    const [alertForm] = useForm();
    const [chainForm] = useForm();

    useEffect(() => {
        ExchangeService.getExchangeData().then((data: IExchangeChain[]) => {
            return setExchangeChainArray(data);
        })
    }, [])

    const getTickers = (chooseAlert: number[] = []) => {
        exchangeArray.current.length = 0;
        setExchangeTickersArray([]);
        chainForm.resetFields(["tickerItems"])
        setTickersIsLoading(true)
        exchangeArray.current.push(...chooseAlert);
        if (exchangeArray.current.length !== 0) {
            ExchangeTicketsService.getTickersData(exchangeArray.current).then((data: IExchangeChainTickers[]) => {
                const dataWithoutDuplicates = data.filter((item, index) => data.indexOf(item) === index)
                setTickersIsLoading(false)
                return setExchangeTickersArray(dataWithoutDuplicates);
            })
        }
    }

    const deleteChainLink = (link: IExchangeChain) => {
        const arrWithoutDeleteLink = chainLinks
        ?.filter((item: IExchangeChain) => item.name !== link.name)
        ?.map((item: IExchangeChain, index: number) => {
            return {
                ...item,
                order: index
            }
        })
        setChainLinks(arrWithoutDeleteLink)
    }

    const finishNewLinkForm = (data: any) => {
        const finishResult: any = [];
        console.log(data, exchangeChainArray);
        exchangeChainArray?.sort((a: any, b: any) => data.exchangeItems.indexOf(a.id) - data.exchangeItems.indexOf(b.id))
            ?.map((item: IExchangeChain, index: number) => {
                data.exchangeItems?.map((itemDataId: number) => {
                    if (itemDataId === item.id) {
                        finishResult.push({
                            ...item,
                            order: index
                        })
                    }
                })
            })
        setChainLinks(finishResult);
        handleLinkCancel()
    }

    const showModal = () => {
        setIsModalVisible(true);
    };

    const showLinkModal = () => {
        setIsLinkModalVisible(true);
    };

    const handleOk = () => {
        setIsModalVisible(false);
    };

    const handleLinkOk = () => {
        setIsLinkModalVisible(false);
    };

    const handleCancel = () => {
        setIsModalVisible(false);
        setChainLinks([])
        alertForm.resetFields()
        chainForm.resetFields()
    };

    const handleLinkCancel = () => {
        setExchangeTickersArray([]);
        chainForm.resetFields()
        setIsLinkModalVisible(false);
    };

    return (
        <Content style={{ display: "flex", flexDirection: "column" }}>
            <div className="notification-container"
                style={{
                    display: "flex",
                    flexDirection: "row",
                    width: "100%",
                    justifyContent: "space-between"
                }}>
                <div className="alerts-container"
                    style={{
                        margin: "0 auto",
                        width: "45%",
                        textAlign: "center"
                    }}>
                    <Card
                        bordered
                    >
                        <p style={{
                            margin: "0",
                            fontSize: "16px"
                        }}>{t("common:Alerts")}</p>
                    </Card>
                    <Button
                        type="primary"
                        onClick={showModal}
                        style={{
                            marginTop: "10px",
                            width: "100%",
                            border: "none",
                            background: "#44AB92"
                        }}>
                        {t("common:createNewAlert")}
                    </Button>
                    <Modal
                        title={t("common:createAlert")}
                        centered
                        forceRender
                        visible={isModalVisible}
                        onOk={handleOk}
                        footer={null}
                        onCancel={handleCancel}
                    >
                        <Form
                            form={alertForm}
                            // onFinish={(data) => changePass(data)}
                            style={{
                                display: "flex",
                                justifyContent: "center",
                                alignItems: "center",
                                flexDirection: "column"
                            }}>
                            <span className="form-description">{`${t("common:Parametrs")}`}</span>
                            <Input.Group size="default">
                                <Row style={{
                                    display: "flex",
                                    justifyContent: "space-around",
                                    alignItems: "center",

                                }} justify="space-around" gutter={8}>
                                    <Col span="8">
                                        <Row style={{
                                            marginTop: "4px",
                                            marginBottom: "4px",
                                            justifyContent: "center"
                                        }}>{`${t("common:Dif")}`}</Row>
                                        <Row><Input type="number" /></Row>
                                        <Row style={{
                                            marginTop: "4px",
                                            marginBottom: "4px",
                                            justifyContent: "center"
                                        }}>{`${t("common:Percent")}`}
                                        </Row>
                                    </Col>
                                    <Col span="8">
                                        <Row style={{
                                            marginTop: "4px",
                                            marginBottom: "4px",
                                            justifyContent: "center"
                                        }}>{`${t("common:24hvol")}`}</Row>
                                        <Row><Input type="number" /></Row>
                                        <Row style={{
                                            marginTop: "4px",
                                            marginBottom: "4px",
                                            justifyContent: "center"
                                        }}>{`${t("common:Dollar")}`}
                                        </Row>
                                    </Col>
                                </Row>
                            </Input.Group>
                            <Form.Item
                                name={"chainLinks"}
                                style={{
                                    width: "100%",
                                }}>
                                {chainLinks && chainLinks?.length >= 1
                                    ? chainLinks.map((item: IExchangeChain) => {
                                        return (<Card
                                            bodyStyle={{
                                                position: "relative",
                                                display: "flex",
                                                justifyContent: "center"
                                            }}
                                            style={{
                                                backgroundColor: "#111C44",
                                                borderRadius: "15px",
                                                padding: "0px",
                                                position: "relative",
                                                textAlign: "center",
                                                marginTop: "16px",
                                            }}
                                            bordered
                                        >
                                            <p
                                                style={{
                                                    margin: "0",
                                                    padding: "0px",
                                                    fontSize: "16px"
                                                }}>
                                                {item.name}
                                            </p>
                                            <CloseCircleOutlined
                                                onClick={() => deleteChainLink(item)}
                                                style={{
                                                    position: "absolute",
                                                    top: "50%",
                                                    left: "90%",
                                                    transform: "translate(-50%, -50%)",
                                                    fontSize: "20px",
                                                    color: "#F81D22",
                                                }} />
                                        </Card>)
                                    })
                                    : <React.Fragment></React.Fragment>
                                }
                            </Form.Item>

                            <span className="form-description">{`${t("common:Chain")}`}</span>
                            <Button
                                type="ghost"
                                onClick={showLinkModal}
                                style={{
                                    marginTop: "10px",
                                    width: "100%",
                                }}>
                                {t("common:createNewAlert")}
                            </Button>
                            <Modal
                                title={t("common:NewLink")}
                                forceRender
                                visible={isLinkModalVisible}
                                onOk={handleLinkOk}
                                footer={null}
                                onCancel={handleLinkCancel}
                            >
                                <Form
                                    form={chainForm}
                                    onFinish={(data) => finishNewLinkForm(data)}
                                    style={{ textAlign: "center" }}
                                >
                                    <span className="form-description">{`${t("common:Exchange")}`}</span>
                                    <Form.Item
                                        name={"exchangeItems"}
                                        rules={[
                                            {
                                                required: true,
                                                message: `${t("common:ThisFieldRequired")}`,
                                            },
                                        ]}>
                                        <Select mode="multiple" onChange={getTickers} allowClear>
                                            {exchangeChainArray?.length && exchangeChainArray?.map((item: IExchangeChain) => {
                                                return (
                                                    <Option

                                                        value={item.id}
                                                        key={item.id}
                                                    >
                                                        {item.name}
                                                    </Option>
                                                )
                                            })}
                                        </Select>
                                    </Form.Item>
                                    <span className="form-description">{`${t("common:Tickers")}`}</span>
                                    <Form.Item
                                        name={"tickerItems"}
                                        rules={[
                                            {
                                                required: true,
                                                message: `${t("common:ThisFieldRequired")}`,
                                            },
                                        ]}>
                                        <Select
                                            allowClear
                                            mode="multiple"
                                            notFoundContent={tickersIsLoading ? <Spin size="default" /> : null}
                                        >
                                            {exchangeTickersArray?.length && exchangeTickersArray?.map((item: IExchangeChainTickers) => {
                                                return (
                                                    <Option
                                                        value={`${item.fromTicker}(${item.toTicker})`}
                                                        key={`${item.fromTicker}(${item.toTicker})`}
                                                    >
                                                        {`${item.fromTicker}(${item.toTicker})`}
                                                    </Option>
                                                )
                                            })}
                                        </Select>
                                    </Form.Item>
                                    <Form.Item
                                        style={{
                                            maxWidth: "220px",
                                            display: "flex"
                                        }}
                                    >
                                        <Button type="primary" htmlType="submit">
                                            {t("common:ButtonCreate")}
                                        </Button>
                                        <Button
                                            style={{
                                                marginLeft: "10px"
                                            }} type="default"
                                            onClick={handleLinkCancel}
                                        >
                                            {t("common:ButtonClose")}
                                        </Button>
                                    </Form.Item>
                                </Form>
                            </Modal>
                        </Form>
                    </Modal>
                </div>
                <div className="notifications-container"
                    style={{
                        margin: "0 auto",
                        width: "45%",
                        textAlign: "center"
                    }}>
                    <Card
                        bordered
                    >
                        <p style={{
                            margin: "0",
                            fontSize: "16px"
                        }}>{t("common:Notification")}</p>
                    </Card>
                </div>
            </div>
        </Content >
    );

}

export default Notification;