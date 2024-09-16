--
-- PostgreSQL database dump
--

-- Dumped from database version 16.2
-- Dumped by pg_dump version 16.2 (Debian 16.2-1.pgdg120+2)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: public; Type: SCHEMA; Schema: -; Owner: azure_pg_admin
--

--CREATE SCHEMA public;


--ALTER SCHEMA public OWNER TO azure_pg_admin;
CREATE ROLE "SethRachelDallin";

--
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: azure_pg_admin
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: car; Type: TABLE; Schema: public; Owner: SethRachelDallin
--

CREATE TABLE public.car (
    id integer NOT NULL,
    cust_id integer NOT NULL,
    make character varying(16) NOT NULL,
    model character varying(32) NOT NULL,
    license_plate character varying(8) NOT NULL,
    year character varying(4) NOT NULL
);


ALTER TABLE public.car OWNER TO "SethRachelDallin";

--
-- Name: car_id_seq; Type: SEQUENCE; Schema: public; Owner: SethRachelDallin
--

CREATE SEQUENCE public.car_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.car_id_seq OWNER TO "SethRachelDallin";

--
-- Name: car_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: SethRachelDallin
--

ALTER SEQUENCE public.car_id_seq OWNED BY public.car.id;


--
-- Name: customer; Type: TABLE; Schema: public; Owner: SethRachelDallin
--

CREATE TABLE public.customer (
    id integer NOT NULL,
    name character varying(64) NOT NULL,
    email character varying(64) NOT NULL,
    password_hash character varying(64),
    phone character varying(10)
);


ALTER TABLE public.customer OWNER TO "SethRachelDallin";

--
-- Name: customer_id_seq; Type: SEQUENCE; Schema: public; Owner: SethRachelDallin
--

CREATE SEQUENCE public.customer_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.customer_id_seq OWNER TO "SethRachelDallin";

--
-- Name: customer_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: SethRachelDallin
--

ALTER SEQUENCE public.customer_id_seq OWNED BY public.customer.id;


--
-- Name: workorder; Type: TABLE; Schema: public; Owner: SethRachelDallin
--

CREATE TABLE public.workorder (
    id integer NOT NULL,
    cust_id integer NOT NULL,
    car_id integer NOT NULL,
    odometer double precision NOT NULL,
    concerns character varying(1024) NOT NULL,
    comments character varying(512),
    datesubmitted character varying(20) NOT NULL
);


ALTER TABLE public.workorder OWNER TO "SethRachelDallin";

--
-- Name: workorder_id_seq; Type: SEQUENCE; Schema: public; Owner: SethRachelDallin
--

CREATE SEQUENCE public.workorder_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.workorder_id_seq OWNER TO "SethRachelDallin";

--
-- Name: workorder_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: SethRachelDallin
--

ALTER SEQUENCE public.workorder_id_seq OWNED BY public.workorder.id;


--
-- Name: car id; Type: DEFAULT; Schema: public; Owner: SethRachelDallin
--

ALTER TABLE ONLY public.car ALTER COLUMN id SET DEFAULT nextval('public.car_id_seq'::regclass);


--
-- Name: customer id; Type: DEFAULT; Schema: public; Owner: SethRachelDallin
--

ALTER TABLE ONLY public.customer ALTER COLUMN id SET DEFAULT nextval('public.customer_id_seq'::regclass);


--
-- Name: workorder id; Type: DEFAULT; Schema: public; Owner: SethRachelDallin
--

ALTER TABLE ONLY public.workorder ALTER COLUMN id SET DEFAULT nextval('public.workorder_id_seq'::regclass);


--
-- Data for Name: car; Type: TABLE DATA; Schema: public; Owner: SethRachelDallin
--

COPY public.car (id, cust_id, make, model, license_plate, year) FROM stdin;
1	1	Toyota	Corolla	ABC123	2019
2	1	Honda	Civic	XYZ789	2020
3	2	Ford	Focus	DEF456	2018
4	2	Chevrolet	Malibu	GHI789	2017
5	2	Nissan	Altima	JKL012	2016
6	1	BMW	X5	MNO345	2021
7	3	Audi	A4	PQR678	2019
8	3	Volkswagen	Jetta	STU901	2018
9	4	Hyundai	Elantra	VWX234	2020
\.


--
-- Data for Name: customer; Type: TABLE DATA; Schema: public; Owner: SethRachelDallin
--

COPY public.customer (id, name, email, password_hash, phone) FROM stdin;
1	John Doe	john.doe@example.com	password123	1234567890
2	Jane Smith	jane.smith@example.com	password456	0987654321
3	Alice Johnson	alice.johnson@example.com	password789	1112223333
4	Bob Williams	bob.williams@example.com	passwordabc	4445556666
5	Emily Davis	emily.davis@example.com	passwordxyz	7778889999
6	Michael Brown	michael.brown@example.com	passworddef	1231231234
\.


--
-- Data for Name: workorder; Type: TABLE DATA; Schema: public; Owner: SethRachelDallin
--

COPY public.workorder (id, cust_id, car_id, odometer, concerns, comments, datesubmitted) FROM stdin;
1	1	1	50000	Engine check	Needs oil change	2024-03-15
2	1	2	60000	Brake inspection	\N	2024-03-20
3	2	3	40000	Transmission service	Completed	2024-03-25
4	2	4	70000	Tire rotation	\N	2024-03-28
5	2	5	55000	Electrical issues	Pending parts order	2024-04-02
6	1	6	30000	Scheduled maintenance	Completed	2024-04-05
7	3	7	45000	Check engine light	Replaced oxygen sensor	2024-04-10
8	3	8	60000	Alignment	Scheduled for next week	2024-04-15
9	4	9	35000	Oil change	In progress	2024-04-20
\.


--
-- Name: car_id_seq; Type: SEQUENCE SET; Schema: public; Owner: SethRachelDallin
--

SELECT pg_catalog.setval('public.car_id_seq', 9, true);


--
-- Name: customer_id_seq; Type: SEQUENCE SET; Schema: public; Owner: SethRachelDallin
--

SELECT pg_catalog.setval('public.customer_id_seq', 6, true);


--
-- Name: workorder_id_seq; Type: SEQUENCE SET; Schema: public; Owner: SethRachelDallin
--

SELECT pg_catalog.setval('public.workorder_id_seq', 9, true);


--
-- Name: car car_pkey; Type: CONSTRAINT; Schema: public; Owner: SethRachelDallin
--

ALTER TABLE ONLY public.car
    ADD CONSTRAINT car_pkey PRIMARY KEY (id);


--
-- Name: workorder customer_car_unique; Type: CONSTRAINT; Schema: public; Owner: SethRachelDallin
--

ALTER TABLE ONLY public.workorder
    ADD CONSTRAINT customer_car_unique UNIQUE (cust_id, car_id);


--
-- Name: customer customer_pkey; Type: CONSTRAINT; Schema: public; Owner: SethRachelDallin
--

ALTER TABLE ONLY public.customer
    ADD CONSTRAINT customer_pkey PRIMARY KEY (id);


--
-- Name: customer email_unique; Type: CONSTRAINT; Schema: public; Owner: SethRachelDallin
--

ALTER TABLE ONLY public.customer
    ADD CONSTRAINT email_unique UNIQUE (email);


--
-- Name: car license_plate_unique; Type: CONSTRAINT; Schema: public; Owner: SethRachelDallin
--

ALTER TABLE ONLY public.car
    ADD CONSTRAINT license_plate_unique UNIQUE (license_plate);


--
-- Name: workorder workorder_pkey; Type: CONSTRAINT; Schema: public; Owner: SethRachelDallin
--

ALTER TABLE ONLY public.workorder
    ADD CONSTRAINT workorder_pkey PRIMARY KEY (id);


--
-- Name: car car_cust_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: SethRachelDallin
--

ALTER TABLE ONLY public.car
    ADD CONSTRAINT car_cust_id_fkey FOREIGN KEY (cust_id) REFERENCES public.customer(id);


--
-- Name: workorder workorder_car_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: SethRachelDallin
--

ALTER TABLE ONLY public.workorder
    ADD CONSTRAINT workorder_car_id_fkey FOREIGN KEY (car_id) REFERENCES public.car(id);


--
-- Name: workorder workorder_cust_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: SethRachelDallin
--

ALTER TABLE ONLY public.workorder
    ADD CONSTRAINT workorder_cust_id_fkey FOREIGN KEY (cust_id) REFERENCES public.customer(id);


--
-- PostgreSQL database dump complete
--

