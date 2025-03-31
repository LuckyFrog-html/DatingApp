"use client";

import Image from "next/image";
import logo from "../../assets/logo.png";
import Link from "next/link";
import { usePathname } from "next/navigation";
import { twMerge } from "tailwind-merge";

const pages = [
    {
        name: "Главная",
        href: "/",
    },
    {
        name: "Искать",
        href: "/search",
    },
    {
        name: "Профиль",
        href: "/profile",
    },
    {
        name: "Достижения",
        href: "/achievements",
    },
];

export const Header = () => {
    const pathname = usePathname();

    return (
        <div className="w-full h-[40px] flex justify-between items-center">
            <Image src={logo} width={200} height={40} alt="logo" />
            <div className="flex gap-5 text-white text-lg font-medium">
                {pages.map((page) => (
                    <Link
                        key={page.href}
                        className={twMerge(
                            'transition-all',
                            pathname === page.href && "tracking-wider font-bold active",
                        )}
                        href={page.href}>
                        {page.name}
                    </Link>
                ))}
                {/* <Link
                    className={twMerge(pathname === "/" && "font-black")}
                    href="/">
                    Главная
                </Link>
                <Link
                    className={twMerge(pathname === "/search" && "font-black")}
                    href="/search">
                    Искать
                </Link>
                <Link
                    className={twMerge(pathname === "/profile" && "font-black")}
                    href="/profile">
                    Профиль
                </Link>
                <Link
                    className={twMerge(
                        pathname === "/achievements" && "font-black",
                    )}
                    href="/achievements">
                    Достижения
                </Link> */}
            </div>
        </div>
    );
};
