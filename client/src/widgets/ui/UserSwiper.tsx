"use client";

import { animateValue } from "@/app/shared/lib/hooks/animate";
import { useWindowCenter } from "@/app/shared/lib/hooks/useWindowScreen";
import { useEffect, useRef, useState } from "react";
import { SwipeEventData, useSwipeable } from "react-swipeable";

type Props = {
    user: any;
    onSwipeEnded?: () => void;
    onSwipeSuccessEnded?: () => void;
    leftRightRatio: number;
    setLeftRightRatio: (val: number) => void;
    like: () => void;
    dislike: () => void;
    clickKludge: number;
};

const X_OFFSET_RATIO = 0.2;
const ROTATION_POINT_RATIO = 0.01;
const CARD_WIDTH = 300;
const CARD_HEIGHT = 500;
const CONTAINER_WIDTH = 1280;
const CARD_ON_SUCCESS_SPEED = 0.3;
const CARD_ON_CLICK_SPEED = 3;

export const UserSwiper = ({
    user,
    onSwipeEnded,
    onSwipeSuccessEnded,
    leftRightRatio,
    setLeftRightRatio,
    dislike,
    like,
    clickKludge,
}: Props) => {
    const center = useWindowCenter();

    const innerRef = useRef<HTMLDivElement>(null);
    const outerRef = useRef<HTMLDivElement>(null);

    const [rotationPoint, setRotationPoint] = useState([0, 0]);
    const [rotation, setRotation] = useState(0);
    const [resXOffset, setResXOffset] = useState(0);
    const [transition, setTransition] = useState(false);
    const [cursorAtLeft, setCursorAtLeft] = useState(false);
    const [yCompensation, setYCompensation] = useState(0);

    const [swipeStarted, setSwipeStarted] = useState(false);

    function changeValue(val: number, setVal: any, time: number) {
        const start = performance.now();

        function update(now: number) {
            const elapsed = now - start;
            // Вычисляем прогресс от 0 до 1
            const progress = Math.min(elapsed / time, 1);
            // Интерполируем значение от 0 до val
            const newValue = progress * val;
            setVal(newValue);
            // Если время ещё не закончилось, продолжаем анимацию
            if (progress < 1) {
                requestAnimationFrame(update);
            }
        }

        requestAnimationFrame(update);
    }

    const leftRightRatioRef = useRef(leftRightRatio);

    // Update the ref on each render when leftRightRatio changes:
    useEffect(() => {
        leftRightRatioRef.current = leftRightRatio;
    }, [leftRightRatio]);

    // useEffect to trigger internal logic when leftRightRatio is set externally:
    useEffect(() => {
        console.log("хуле ты прокаешь еблан");
        if (
            leftRightRatioRef.current === 1 ||
            leftRightRatioRef.current === -1
        ) {
            // logic to run for "like"
            console.log(
                "UserSwiper: Like triggered via external button",
                clickKludge,
            );
            // For instance, run an animation or update internal state:
            // (You can even call successHandler() or any function)
            successHandler(true);
            // Optionally, reset state if needed:
            // setLeftRightRatio(0);
        } else if (leftRightRatio === -1) {
            // logic to run for "dislike"
            console.log("UserSwiper: Dislike triggered via external button");
            successHandler(true);
            // setLeftRightRatio(0);
        }
    }, [clickKludge]); // dependency array

    useEffect(() => {
        swipeEndedHandler();
        setRotationPoint([center[0], center[1]]);
    }, [center]);

    const swipingHandler = (eventData: SwipeEventData) => {
        const e = eventData;
        const deltaMidX = eventData.initial[0] + eventData.deltaX - center[0];
        const deltaMidY = eventData.initial[1] + eventData.deltaY - center[1];

        const x = e.initial[0] + e.deltaX - center[0];
        const y = e.initial[1] + e.deltaY - center[1];
        const hypotenuse = Math.sqrt(x * x + y * y);
        let angle = Math.atan2(y, x);

        if (deltaMidX < 0) {
            if (deltaMidY < 0) {
                angle = Math.PI + angle;
            } else {
                angle = -(Math.PI - angle);
            }
        }

        const rotAngle = -angle * 0.2;

        if (rotAngle > 0) {
            setRotation(Math.min(rotAngle, 0.2));
        } else {
            setRotation(Math.max(rotAngle, -0.2));
        }
        // setRotation(-angle * 0.2);
        // if (deltaMidY > 0) {
        //     setRotation(-deltaMidX * ROTATION_POINT_RATIO);
        // } else {
        //     setRotation(deltaMidX * ROTATION_POINT_RATIO);
        // }
        // setRotationPoint([CARD_WIDTH / 2, CARD_HEIGHT / 2 + deltaMidY]);

        // console.log(x, y, y / hypotenuse);

        // const deltaBetweenCenterAndCursorX = e.initial[0] + e.deltaX;

        // const vec = [];
        // setYCompensation(-(center[1] + deltaMidY));

        setResXOffset(deltaMidX * X_OFFSET_RATIO);
        // console.log(deltaMidX, CONTAINER_WIDTH / 2, deltaMidX / (CONTAINER_WIDTH / 2), Math.min(deltaMidX / (CONTAINER_WIDTH / 2), 1), Math.max(Math.min(deltaMidX / (CONTAINER_WIDTH / 2), 1), -1));
        // if ()
        setLeftRightRatio(
            Math.max(Math.min(deltaMidX / (CONTAINER_WIDTH / 2), 1), -1),
        );
    };

    const swipeStartedHandler = (eventData: SwipeEventData) => {
        // setSwipeStarted(true);
        // setTimeout(() => setSwipeStarted(false), 300);
        setTransition(false);
        // giveTempTransition();
    };

    const swipeEndedHandler = () => {
        if (leftRightRatio === 1 || leftRightRatio === -1) {
            onSwipeSuccessEnded?.();
            successHandler();

            // if (leftRightRatio === 1) {
            //     like();
            // } else {
            //     dislike();
            // }

            return;
        }

        onSwipeEnded?.();
        setTransition(true);
        setResXOffset(0);
        setRotation(0);
        setRotationPoint([CARD_WIDTH / 2, CARD_HEIGHT / 2]);
        setLeftRightRatio(0);
    };

    const handlers = useSwipeable({
        onSwipeStart: swipeStartedHandler,
        onSwiped: swipeEndedHandler,
        onSwiping: swipingHandler,
        trackMouse: true,
    });

    const successHandler = (isClick = false) => {
        console.log("helo is click", isClick);
        // setTransition(true);
        setRotation(0);
        animateValue({
            from: 0,
            to: 1,
            duration: 0.6,
            // duration: isClick ? 0.6 : 0.4,
            onUpdate: (val) => {
                // console.log(
                //     "!isClick",
                //     !isClick,
                //     !isClick ? CARD_ON_SUCCESS_SPEED : CARD_ON_CLICK_SPEED,
                //     ((val * window.innerWidth) / 2) *
                //         (!isClick
                //             ? CARD_ON_SUCCESS_SPEED
                //             : CARD_ON_CLICK_SPEED),
                //     leftRightRatio > 0
                //         ? resXOffset +
                //               ((val * window.innerWidth) / 2) *
                //                   (!isClick
                //                       ? CARD_ON_SUCCESS_SPEED
                //                       : CARD_ON_CLICK_SPEED)
                //         : resXOffset -
                //               ((val * window.innerWidth) / 2) *
                //                   (!isClick
                //                       ? CARD_ON_SUCCESS_SPEED
                //                       : CARD_ON_CLICK_SPEED),
                // );
                setResXOffset((prev) =>
                    leftRightRatio > 0
                        ? prev +
                          ((val * window.innerWidth) / 2) *
                              (isClick
                                  ? CARD_ON_SUCCESS_SPEED
                                  : CARD_ON_CLICK_SPEED)
                        : prev -
                          ((val * window.innerWidth) / 2) *
                              (isClick
                                  ? CARD_ON_SUCCESS_SPEED
                                  : CARD_ON_CLICK_SPEED),
                );
            },
            onComplete: () => {
                console.log("onComplete");
                setResXOffset(0);
            },
        });
        animateValue({
            from: 1,
            to: 0,
            // duration: isClick ? 0.2 : 0.3,
            duration: 0.2,
            onUpdate: (val) => {
                if (!outerRef.current) return;
                outerRef.current.style.opacity = val.toString();
            },
        });
        setTimeout(
            () => {
                if (!outerRef.current) return;
                console.log("setTimeout");
                outerRef.current.style.opacity = "1";
                setTransition(true);
            },
            // isClick ? 800 : 500,
            800,
        );
    };

    return (
        <div
            {...handlers}
            className="z-10 relative h-[calc(100vh-60px)] flex flex-col items-center justify-center">
            <div
                ref={outerRef}
                style={{
                    transform: `translateX(${resXOffset}px)`,
                    transition: transition ? "all 0.3s ease" : "none",
                }}
                // className="bg-green-500"
            >
                {/* Inner container: applies rotation with custom transform origin */}
                <div
                    ref={innerRef}
                    style={{
                        transform: `rotate(${rotation}rad)`,
                        // transformOrigin: `${rotationPoint[0]}px ${rotationPoint[1]}px`,
                        transition: transition ? "all 0.3s ease" : "none",
                        width: `${CARD_WIDTH}px`,
                        height: `${CARD_HEIGHT}px`,
                    }}
                    className={`rounded-2xl bg-blue-500`}
                />
            </div>
            {/* <div
                style={{
                    transform: `translateX(${rotationPoint[0]}px) translateY(${rotationPoint[1]}px)`,
                }}
                className="w-[1px] h-[1px] bg-amber-200 z-20"></div> */}
        </div>
    );
};
