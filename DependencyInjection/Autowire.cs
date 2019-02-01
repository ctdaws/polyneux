
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Polyneux
{
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class Autowire : System.Attribute
    {
    }
}